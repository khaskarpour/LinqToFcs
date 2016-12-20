using LINQPad.Extensibility.DataContext;
using LinqToFcs.Core;
using LinqToFcs.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LinqToFcs.LinqPadDriver
{
    public class LinqToFcsDriver : StaticDataContextDriver 
    {
        public override string Author
        {
            get { return "Khashayar Askarpour"; }
        }

        public override string Name
        {
            get { return "Linq To Fcs Driver"; }
        }

        public override bool AreRepositoriesEquivalent(IConnectionInfo c1, IConnectionInfo c2)
        {
            return c1.DatabaseInfo.AttachFileName == c2.DatabaseInfo.AttachFileName;
        }

        public override ParameterDescriptor[] GetContextConstructorParameters(IConnectionInfo cxInfo)
        {
            // We need to pass the filename into the FcsContext's constructor:
            return new[] { new ParameterDescriptor("filenam", typeof(string).FullName) };
        }

        public override string GetConnectionDescription(IConnectionInfo cxInfo)
        {
            // For static drivers, we can use the description of the custom type & its assembly:
            return cxInfo.DatabaseInfo.AttachFileName;
        }

        public override object[] GetContextConstructorArguments(IConnectionInfo cxInfo)
        {
            return new object[] { cxInfo.DatabaseInfo.AttachFileName };
        }

        public override void InitializeContext(IConnectionInfo cxInfo, object context, QueryExecutionManager executionManager)
        {
            base.InitializeContext(cxInfo, context, executionManager);
        }

        public override bool ShowConnectionDialog(IConnectionInfo cxInfo, bool isNewConnection)
        {
            return new ConnectionDialog(cxInfo).ShowDialog() == true;
        }

        public override void OnQueryFinishing(IConnectionInfo cxInfo, object context, QueryExecutionManager executionManager)
        {
            base.OnQueryFinishing(cxInfo, context, executionManager);
        }

        public override void DisplayObjectInGrid(object objectToDisplay, GridOptions options)
        {
            base.DisplayObjectInGrid(objectToDisplay, options);
        }

        public override List<ExplorerItem> GetSchema(IConnectionInfo cxInfo, Type customType)
        {
            using (FcsReader context = new FcsReader(cxInfo.DatabaseInfo.AttachFileName))
            {
                List<ExplorerItem> nodes = new List<ExplorerItem>();

                for (int i = 0; i < context.DataSets.Count; ++i)
                {
                    ExplorerItem headerNode = new ExplorerItem("Header", ExplorerItemKind.Property, ExplorerIcon.OneToOne)
                    {
                        Children = GetObjectChildren(context.DataSets[i].HeaderData)
                    };

                    ExplorerItem parametersNode = new ExplorerItem("Parameters", ExplorerItemKind.QueryableObject, ExplorerIcon.Table)
                    {
                        Children = GetTypeChildren(typeof(ParameterData)),
                        IsEnumerable = true
                    };

                    ExplorerItem textNode = new ExplorerItem("Text", ExplorerItemKind.Property, ExplorerIcon.OneToOne)
                    {
                        Children = new List<ExplorerItem> { parametersNode }.Concat(GetObjectChildren(context.DataSets[i].TextData, p => p.Name != "Parameters")).ToList()
                    };

                    ExplorerItem eventNode = new ExplorerItem("Events", ExplorerItemKind.QueryableObject, ExplorerIcon.Table)
                    {
                        IsEnumerable = true
                    };

                    nodes.Add(new ExplorerItem(string.Format("DataSet{0}", i + 1), ExplorerItemKind.Property, ExplorerIcon.OneToMany)
                    {
                        Children = new List<ExplorerItem>
                        {
                            headerNode,
                            textNode,
                            eventNode
                        }
                    });
                }

                return nodes;
            }
        }

        private List<ExplorerItem> GetObjectChildren(object obj, Func<PropertyInfo, bool> where = null)
        {
            IEnumerable<PropertyInfo> orderedProps = obj
                .GetType()
                .GetProperties();

            if (where != null)
            {
                orderedProps = orderedProps.Where(where);
            }

            return orderedProps
                .OrderBy(x => x.Name)
                .Select(x => new ExplorerItem(string.Format("{0} = {1}", x.Name, x.GetValue(obj)), ExplorerItemKind.Property, ExplorerIcon.Parameter))
                .ToList();
        }

        private List<ExplorerItem> GetTypeChildren(Type type, Func<PropertyInfo, bool> where = null)
        {
            IEnumerable<PropertyInfo> orderedProps = type
                .GetProperties();

            if (where != null)
            {
                orderedProps = orderedProps.Where(where);
            }

            return orderedProps
                .OrderBy(x => x.Name)
                .Select(x => new ExplorerItem(string.Format("{0}", x.Name), ExplorerItemKind.Property, ExplorerIcon.Column))
                .ToList();
        }
    }
}
