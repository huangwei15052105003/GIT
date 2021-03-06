﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.573
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

// Downloads By http://www.veryhuo.com
namespace star {
    using System;
    using System.Data;
    using System.Xml;
    using System.Runtime.Serialization;
    
    
    [Serializable()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.ToolboxItem(true)]
    public class Dataset1 : DataSet {
        
        private IN020DataTable tableIN020;
        
        public Dataset1() {
            this.InitClass();
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            this.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        protected Dataset1(SerializationInfo info, StreamingContext context) {
            string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
            if ((strSchema != null)) {
                DataSet ds = new DataSet();
                ds.ReadXmlSchema(new XmlTextReader(new System.IO.StringReader(strSchema)));
                if ((ds.Tables["IN020"] != null)) {
                    this.Tables.Add(new IN020DataTable(ds.Tables["IN020"]));
                }
                this.DataSetName = ds.DataSetName;
                this.Prefix = ds.Prefix;
                this.Namespace = ds.Namespace;
                this.Locale = ds.Locale;
                this.CaseSensitive = ds.CaseSensitive;
                this.EnforceConstraints = ds.EnforceConstraints;
                this.Merge(ds, false, System.Data.MissingSchemaAction.Add);
                this.InitVars();
            }
            else {
                this.InitClass();
            }
            this.GetSerializationData(info, context);
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            this.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Content)]
        public IN020DataTable IN020 {
            get {
                return this.tableIN020;
            }
        }
        
        public override DataSet Clone() {
            Dataset1 cln = ((Dataset1)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override bool ShouldSerializeTables() {
            return false;
        }
        
        protected override bool ShouldSerializeRelations() {
            return false;
        }
        
        protected override void ReadXmlSerializable(XmlReader reader) {
            this.Reset();
            DataSet ds = new DataSet();
            ds.ReadXml(reader);
            if ((ds.Tables["IN020"] != null)) {
                this.Tables.Add(new IN020DataTable(ds.Tables["IN020"]));
            }
            this.DataSetName = ds.DataSetName;
            this.Prefix = ds.Prefix;
            this.Namespace = ds.Namespace;
            this.Locale = ds.Locale;
            this.CaseSensitive = ds.CaseSensitive;
            this.EnforceConstraints = ds.EnforceConstraints;
            this.Merge(ds, false, System.Data.MissingSchemaAction.Add);
            this.InitVars();
        }
        
        protected override System.Xml.Schema.XmlSchema GetSchemaSerializable() {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            this.WriteXmlSchema(new XmlTextWriter(stream, null));
            stream.Position = 0;
            return System.Xml.Schema.XmlSchema.Read(new XmlTextReader(stream), null);
        }
        
        internal void InitVars() {
            this.tableIN020 = ((IN020DataTable)(this.Tables["IN020"]));
            if ((this.tableIN020 != null)) {
                this.tableIN020.InitVars();
            }
        }
        
        private void InitClass() {
            this.DataSetName = "Dataset1";
            this.Prefix = "";
            this.Namespace = "http://tempuri.org/Dataset1.xsd";
            this.Locale = new System.Globalization.CultureInfo("en-US");
            this.CaseSensitive = false;
            this.EnforceConstraints = true;
            this.tableIN020 = new IN020DataTable();
            this.Tables.Add(this.tableIN020);
        }
        
        private bool ShouldSerializeIN020() {
            return false;
        }
        
        private void SchemaChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e) {
            if ((e.Action == System.ComponentModel.CollectionChangeAction.Remove)) {
                this.InitVars();
            }
        }
        
        public delegate void IN020RowChangeEventHandler(object sender, IN020RowChangeEvent e);
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class IN020DataTable : DataTable, System.Collections.IEnumerable {
            
            private DataColumn columnC_DATE;
            
            private DataColumn columnPR_NAME;
            
            private DataColumn columnRE_NAME;
            
            private DataColumn columnPRICE;
            
            private DataColumn columnQTY;
            
            private DataColumn columnTA;
            
            internal IN020DataTable() : 
                    base("IN020") {
                this.InitClass();
            }
            
            internal IN020DataTable(DataTable table) : 
                    base(table.TableName) {
                if ((table.CaseSensitive != table.DataSet.CaseSensitive)) {
                    this.CaseSensitive = table.CaseSensitive;
                }
                if ((table.Locale.ToString() != table.DataSet.Locale.ToString())) {
                    this.Locale = table.Locale;
                }
                if ((table.Namespace != table.DataSet.Namespace)) {
                    this.Namespace = table.Namespace;
                }
                this.Prefix = table.Prefix;
                this.MinimumCapacity = table.MinimumCapacity;
                this.DisplayExpression = table.DisplayExpression;
            }
            
            [System.ComponentModel.Browsable(false)]
            public int Count {
                get {
                    return this.Rows.Count;
                }
            }
            
            internal DataColumn C_DATEColumn {
                get {
                    return this.columnC_DATE;
                }
            }
            
            internal DataColumn PR_NAMEColumn {
                get {
                    return this.columnPR_NAME;
                }
            }
            
            internal DataColumn RE_NAMEColumn {
                get {
                    return this.columnRE_NAME;
                }
            }
            
            internal DataColumn PRICEColumn {
                get {
                    return this.columnPRICE;
                }
            }
            
            internal DataColumn QTYColumn {
                get {
                    return this.columnQTY;
                }
            }
            
            internal DataColumn TAColumn {
                get {
                    return this.columnTA;
                }
            }
            
            public IN020Row this[int index] {
                get {
                    return ((IN020Row)(this.Rows[index]));
                }
            }
            
            public event IN020RowChangeEventHandler IN020RowChanged;
            
            public event IN020RowChangeEventHandler IN020RowChanging;
            
            public event IN020RowChangeEventHandler IN020RowDeleted;
            
            public event IN020RowChangeEventHandler IN020RowDeleting;
            
            public void AddIN020Row(IN020Row row) {
                this.Rows.Add(row);
            }
            
            public IN020Row AddIN020Row(System.DateTime C_DATE, string PR_NAME, string RE_NAME, System.Decimal PRICE, int QTY, System.Decimal TA) {
                IN020Row rowIN020Row = ((IN020Row)(this.NewRow()));
                rowIN020Row.ItemArray = new object[] {
                        C_DATE,
                        PR_NAME,
                        RE_NAME,
                        PRICE,
                        QTY,
                        TA};
                this.Rows.Add(rowIN020Row);
                return rowIN020Row;
            }
            
            public System.Collections.IEnumerator GetEnumerator() {
                return this.Rows.GetEnumerator();
            }
            
            public override DataTable Clone() {
                IN020DataTable cln = ((IN020DataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            
            protected override DataTable CreateInstance() {
                return new IN020DataTable();
            }
            
            internal void InitVars() {
                this.columnC_DATE = this.Columns["C_DATE"];
                this.columnPR_NAME = this.Columns["PR_NAME"];
                this.columnRE_NAME = this.Columns["RE_NAME"];
                this.columnPRICE = this.Columns["PRICE"];
                this.columnQTY = this.Columns["QTY"];
                this.columnTA = this.Columns["TA"];
            }
            
            private void InitClass() {
                this.columnC_DATE = new DataColumn("C_DATE", typeof(System.DateTime), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnC_DATE);
                this.columnPR_NAME = new DataColumn("PR_NAME", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnPR_NAME);
                this.columnRE_NAME = new DataColumn("RE_NAME", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnRE_NAME);
                this.columnPRICE = new DataColumn("PRICE", typeof(System.Decimal), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnPRICE);
                this.columnQTY = new DataColumn("QTY", typeof(int), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnQTY);
                this.columnTA = new DataColumn("TA", typeof(System.Decimal), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnTA);
            }
            // Downloads By http://www.veryhuo.com
            public IN020Row NewIN020Row() {
                return ((IN020Row)(this.NewRow()));
            }
            
            protected override DataRow NewRowFromBuilder(DataRowBuilder builder) {
                return new IN020Row(builder);
            }
            
            protected override System.Type GetRowType() {
                return typeof(IN020Row);
            }
            
            protected override void OnRowChanged(DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.IN020RowChanged != null)) {
                    this.IN020RowChanged(this, new IN020RowChangeEvent(((IN020Row)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowChanging(DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.IN020RowChanging != null)) {
                    this.IN020RowChanging(this, new IN020RowChangeEvent(((IN020Row)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleted(DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.IN020RowDeleted != null)) {
                    this.IN020RowDeleted(this, new IN020RowChangeEvent(((IN020Row)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleting(DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.IN020RowDeleting != null)) {
                    this.IN020RowDeleting(this, new IN020RowChangeEvent(((IN020Row)(e.Row)), e.Action));
                }
            }
            
            public void RemoveIN020Row(IN020Row row) {
                this.Rows.Remove(row);
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class IN020Row : DataRow {
            
            private IN020DataTable tableIN020;
            
            internal IN020Row(DataRowBuilder rb) : 
                    base(rb) {
                this.tableIN020 = ((IN020DataTable)(this.Table));
            }
            
            public System.DateTime C_DATE {
                get {
                    try {
                        return ((System.DateTime)(this[this.tableIN020.C_DATEColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableIN020.C_DATEColumn] = value;
                }
            }
            
            public string PR_NAME {
                get {
                    try {
                        return ((string)(this[this.tableIN020.PR_NAMEColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableIN020.PR_NAMEColumn] = value;
                }
            }
            
            public string RE_NAME {
                get {
                    try {
                        return ((string)(this[this.tableIN020.RE_NAMEColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableIN020.RE_NAMEColumn] = value;
                }
            }
            
            public System.Decimal PRICE {
                get {
                    try {
                        return ((System.Decimal)(this[this.tableIN020.PRICEColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableIN020.PRICEColumn] = value;
                }
            }
            
            public int QTY {
                get {
                    try {
                        return ((int)(this[this.tableIN020.QTYColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableIN020.QTYColumn] = value;
                }
            }
            
            public System.Decimal TA {
                get {
                    try {
                        return ((System.Decimal)(this[this.tableIN020.TAColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableIN020.TAColumn] = value;
                }
            }
            
            public bool IsC_DATENull() {
                return this.IsNull(this.tableIN020.C_DATEColumn);
            }
            
            public void SetC_DATENull() {
                this[this.tableIN020.C_DATEColumn] = System.Convert.DBNull;
            }
            
            public bool IsPR_NAMENull() {
                return this.IsNull(this.tableIN020.PR_NAMEColumn);
            }
            
            public void SetPR_NAMENull() {
                this[this.tableIN020.PR_NAMEColumn] = System.Convert.DBNull;
            }
            
            public bool IsRE_NAMENull() {
                return this.IsNull(this.tableIN020.RE_NAMEColumn);
            }
            
            public void SetRE_NAMENull() {
                this[this.tableIN020.RE_NAMEColumn] = System.Convert.DBNull;
            }
            
            public bool IsPRICENull() {
                return this.IsNull(this.tableIN020.PRICEColumn);
            }
            
            public void SetPRICENull() {
                this[this.tableIN020.PRICEColumn] = System.Convert.DBNull;
            }
            
            public bool IsQTYNull() {
                return this.IsNull(this.tableIN020.QTYColumn);
            }
            
            public void SetQTYNull() {
                this[this.tableIN020.QTYColumn] = System.Convert.DBNull;
            }
            
            public bool IsTANull() {
                return this.IsNull(this.tableIN020.TAColumn);
            }
            
            public void SetTANull() {
                this[this.tableIN020.TAColumn] = System.Convert.DBNull;
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class IN020RowChangeEvent : EventArgs {
            
            private IN020Row eventRow;
            
            private DataRowAction eventAction;
            
            public IN020RowChangeEvent(IN020Row row, DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            
            public IN020Row Row {
                get {
                    return this.eventRow;
                }
            }
            
            public DataRowAction Action {
                get {
                    return this.eventAction;
                }
            }
        }
    }
}
