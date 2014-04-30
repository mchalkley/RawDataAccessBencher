﻿///////////////////////////////////////////////////////////////
// This is generated code. 
//////////////////////////////////////////////////////////////
// Code is generated using LLBLGen Pro version: 4.2
// Code is generated on: 
// Code is generated using templates: SD.TemplateBindings.SharedTemplates
// Templates vendor: Solutions Design.
// Templates version: 
//////////////////////////////////////////////////////////////
using System;
using System.ComponentModel;
using System.Data;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using AdventureWorks.Dal.Adapter.v42;
using AdventureWorks.Dal.Adapter.v42.FactoryClasses;
using AdventureWorks.Dal.Adapter.v42.HelperClasses;
using SD.LLBLGen.Pro.ORMSupportClasses;


namespace AdventureWorks.Dal.Adapter.v42.TypedViewClasses
{
	// __LLBLGENPRO_USER_CODE_REGION_START AdditionalNamespaces
	// __LLBLGENPRO_USER_CODE_REGION_END
	
	
	/// <summary>Typed datatable for the view 'Soh'.<br/><br/></summary>
	[Serializable, System.ComponentModel.DesignerCategory("Code")]
	[ToolboxItem(true)]
	[DesignTimeVisible(true)]
	public partial class SohTypedView : TypedViewBase<SohRow>, ITypedView2
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfacesView
		// __LLBLGENPRO_USER_CODE_REGION_END
			
	{
		#region Class Member Declarations
		private DataColumn _columnSalesOrderId;
		private DataColumn _columnRevisionNumber;
		private DataColumn _columnOrderDate;
		private DataColumn _columnDueDate;
		private DataColumn _columnShipDate;
		private DataColumn _columnStatus;
		private DataColumn _columnOnlineOrderFlag;
		private DataColumn _columnSalesOrderNumber;
		private DataColumn _columnPurchaseOrderNumber;
		private DataColumn _columnAccountNumber;
		private DataColumn _columnCustomerId;
		private DataColumn _columnSalesPersonId;
		private DataColumn _columnTerritoryId;
		private DataColumn _columnBillToAddressId;
		private DataColumn _columnShipToAddressId;
		private DataColumn _columnShipMethodId;
		private DataColumn _columnCreditCardId;
		private DataColumn _columnCreditCardApprovalCode;
		private DataColumn _columnCurrencyRateId;
		private DataColumn _columnSubTotal;
		private DataColumn _columnTaxAmt;
		private DataColumn _columnFreight;
		private DataColumn _columnTotalDue;
		private DataColumn _columnComment;
		private DataColumn _columnRowguid;
		private DataColumn _columnModifiedDate;
		private IEntityFields2	_fields;
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalMembers
		// __LLBLGENPRO_USER_CODE_REGION_END
		
		private static Hashtable	_customProperties;
		private static Hashtable	_fieldsCustomProperties;
		#endregion

		#region Class Constants
		/// <summary>
		/// The amount of fields in the resultset.
		/// </summary>
		private const int AmountOfFields = 26;
		#endregion

		/// <summary>Static CTor for setting up custom property hashtables.</summary>
		static SohTypedView()
		{
			SetupCustomPropertyHashtables();
		}

		/// <summary>CTor</summary>
		public SohTypedView():base("Soh")
		{
			InitClass();
		}
#if !CF	
		/// <summary>Protected constructor for deserialization.</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected SohTypedView(SerializationInfo info, StreamingContext context):base(info, context)
		{
			if (SerializationHelper.Optimization == SerializationOptimization.None)
			{
				InitMembers();
			}
		}
#endif
		/// <summary>Gets the IEntityFields2 collection of fields of this typed view. </summary>
		/// <returns>Ready to use IEntityFields2 collection object.</returns>
		public virtual IEntityFields2 GetFieldsInfo()
		{
			return _fields;
		}

		/// <summary>Creates a new typed row during the build of the datatable during a Fill session by a dataadapter.</summary>
		/// <param name="rowBuilder">supplied row builder to pass to the typed row</param>
		/// <returns>the new typed datarow</returns>
		protected override DataRow NewRowFromBuilder(DataRowBuilder rowBuilder) 
		{
			return new SohRow(rowBuilder);
		}

		/// <summary>Initializes the hashtables for the typed view type and typed view field custom properties. </summary>
		private static void SetupCustomPropertyHashtables()
		{
			_customProperties = new Hashtable();
			_fieldsCustomProperties = new Hashtable();
			Hashtable fieldHashtable;
			fieldHashtable = new Hashtable();
			_fieldsCustomProperties.Add("SalesOrderId", fieldHashtable);
			fieldHashtable = new Hashtable();
			_fieldsCustomProperties.Add("RevisionNumber", fieldHashtable);
			fieldHashtable = new Hashtable();
			_fieldsCustomProperties.Add("OrderDate", fieldHashtable);
			fieldHashtable = new Hashtable();
			_fieldsCustomProperties.Add("DueDate", fieldHashtable);
			fieldHashtable = new Hashtable();
			_fieldsCustomProperties.Add("ShipDate", fieldHashtable);
			fieldHashtable = new Hashtable();
			_fieldsCustomProperties.Add("Status", fieldHashtable);
			fieldHashtable = new Hashtable();
			_fieldsCustomProperties.Add("OnlineOrderFlag", fieldHashtable);
			fieldHashtable = new Hashtable();
			_fieldsCustomProperties.Add("SalesOrderNumber", fieldHashtable);
			fieldHashtable = new Hashtable();
			_fieldsCustomProperties.Add("PurchaseOrderNumber", fieldHashtable);
			fieldHashtable = new Hashtable();
			_fieldsCustomProperties.Add("AccountNumber", fieldHashtable);
			fieldHashtable = new Hashtable();
			_fieldsCustomProperties.Add("CustomerId", fieldHashtable);
			fieldHashtable = new Hashtable();
			_fieldsCustomProperties.Add("SalesPersonId", fieldHashtable);
			fieldHashtable = new Hashtable();
			_fieldsCustomProperties.Add("TerritoryId", fieldHashtable);
			fieldHashtable = new Hashtable();
			_fieldsCustomProperties.Add("BillToAddressId", fieldHashtable);
			fieldHashtable = new Hashtable();
			_fieldsCustomProperties.Add("ShipToAddressId", fieldHashtable);
			fieldHashtable = new Hashtable();
			_fieldsCustomProperties.Add("ShipMethodId", fieldHashtable);
			fieldHashtable = new Hashtable();
			_fieldsCustomProperties.Add("CreditCardId", fieldHashtable);
			fieldHashtable = new Hashtable();
			_fieldsCustomProperties.Add("CreditCardApprovalCode", fieldHashtable);
			fieldHashtable = new Hashtable();
			_fieldsCustomProperties.Add("CurrencyRateId", fieldHashtable);
			fieldHashtable = new Hashtable();
			_fieldsCustomProperties.Add("SubTotal", fieldHashtable);
			fieldHashtable = new Hashtable();
			_fieldsCustomProperties.Add("TaxAmt", fieldHashtable);
			fieldHashtable = new Hashtable();
			_fieldsCustomProperties.Add("Freight", fieldHashtable);
			fieldHashtable = new Hashtable();
			_fieldsCustomProperties.Add("TotalDue", fieldHashtable);
			fieldHashtable = new Hashtable();
			_fieldsCustomProperties.Add("Comment", fieldHashtable);
			fieldHashtable = new Hashtable();
			_fieldsCustomProperties.Add("Rowguid", fieldHashtable);
			fieldHashtable = new Hashtable();
			_fieldsCustomProperties.Add("ModifiedDate", fieldHashtable);
		}

		/// <summary>
		/// Initialize the datastructures.
		/// </summary>
		protected override void InitClass()
		{
			TableName = "Soh";		
			_columnSalesOrderId = GeneralUtils.CreateTypedDataTableColumn("SalesOrderId", @"SalesOrderId", typeof(System.Int32), this.Columns);
			_columnRevisionNumber = GeneralUtils.CreateTypedDataTableColumn("RevisionNumber", @"RevisionNumber", typeof(System.Byte), this.Columns);
			_columnOrderDate = GeneralUtils.CreateTypedDataTableColumn("OrderDate", @"OrderDate", typeof(System.DateTime), this.Columns);
			_columnDueDate = GeneralUtils.CreateTypedDataTableColumn("DueDate", @"DueDate", typeof(System.DateTime), this.Columns);
			_columnShipDate = GeneralUtils.CreateTypedDataTableColumn("ShipDate", @"ShipDate", typeof(System.DateTime), this.Columns);
			_columnStatus = GeneralUtils.CreateTypedDataTableColumn("Status", @"Status", typeof(System.Byte), this.Columns);
			_columnOnlineOrderFlag = GeneralUtils.CreateTypedDataTableColumn("OnlineOrderFlag", @"OnlineOrderFlag", typeof(System.Boolean), this.Columns);
			_columnSalesOrderNumber = GeneralUtils.CreateTypedDataTableColumn("SalesOrderNumber", @"SalesOrderNumber", typeof(System.String), this.Columns);
			_columnPurchaseOrderNumber = GeneralUtils.CreateTypedDataTableColumn("PurchaseOrderNumber", @"PurchaseOrderNumber", typeof(System.String), this.Columns);
			_columnAccountNumber = GeneralUtils.CreateTypedDataTableColumn("AccountNumber", @"AccountNumber", typeof(System.String), this.Columns);
			_columnCustomerId = GeneralUtils.CreateTypedDataTableColumn("CustomerId", @"CustomerId", typeof(System.Int32), this.Columns);
			_columnSalesPersonId = GeneralUtils.CreateTypedDataTableColumn("SalesPersonId", @"SalesPersonId", typeof(System.Int32), this.Columns);
			_columnTerritoryId = GeneralUtils.CreateTypedDataTableColumn("TerritoryId", @"TerritoryId", typeof(System.Int32), this.Columns);
			_columnBillToAddressId = GeneralUtils.CreateTypedDataTableColumn("BillToAddressId", @"BillToAddressId", typeof(System.Int32), this.Columns);
			_columnShipToAddressId = GeneralUtils.CreateTypedDataTableColumn("ShipToAddressId", @"ShipToAddressId", typeof(System.Int32), this.Columns);
			_columnShipMethodId = GeneralUtils.CreateTypedDataTableColumn("ShipMethodId", @"ShipMethodId", typeof(System.Int32), this.Columns);
			_columnCreditCardId = GeneralUtils.CreateTypedDataTableColumn("CreditCardId", @"CreditCardId", typeof(System.Int32), this.Columns);
			_columnCreditCardApprovalCode = GeneralUtils.CreateTypedDataTableColumn("CreditCardApprovalCode", @"CreditCardApprovalCode", typeof(System.String), this.Columns);
			_columnCurrencyRateId = GeneralUtils.CreateTypedDataTableColumn("CurrencyRateId", @"CurrencyRateId", typeof(System.Int32), this.Columns);
			_columnSubTotal = GeneralUtils.CreateTypedDataTableColumn("SubTotal", @"SubTotal", typeof(System.Decimal), this.Columns);
			_columnTaxAmt = GeneralUtils.CreateTypedDataTableColumn("TaxAmt", @"TaxAmt", typeof(System.Decimal), this.Columns);
			_columnFreight = GeneralUtils.CreateTypedDataTableColumn("Freight", @"Freight", typeof(System.Decimal), this.Columns);
			_columnTotalDue = GeneralUtils.CreateTypedDataTableColumn("TotalDue", @"TotalDue", typeof(System.Decimal), this.Columns);
			_columnComment = GeneralUtils.CreateTypedDataTableColumn("Comment", @"Comment", typeof(System.String), this.Columns);
			_columnRowguid = GeneralUtils.CreateTypedDataTableColumn("Rowguid", @"Rowguid", typeof(System.Guid), this.Columns);
			_columnModifiedDate = GeneralUtils.CreateTypedDataTableColumn("ModifiedDate", @"ModifiedDate", typeof(System.DateTime), this.Columns);
			_fields = EntityFieldsFactory.CreateTypedViewEntityFieldsObject(TypedViewType.SohTypedView);
			
			// __LLBLGENPRO_USER_CODE_REGION_START AdditionalFields
			// be sure to call _fields.Expand(number of new fields) first. 
			// __LLBLGENPRO_USER_CODE_REGION_END
			
			OnInitialized();
		}

		/// <summary>Initializes the members, after a clone action.</summary>
		private void InitMembers()
		{
			_columnSalesOrderId = this.Columns["SalesOrderId"];
			_columnRevisionNumber = this.Columns["RevisionNumber"];
			_columnOrderDate = this.Columns["OrderDate"];
			_columnDueDate = this.Columns["DueDate"];
			_columnShipDate = this.Columns["ShipDate"];
			_columnStatus = this.Columns["Status"];
			_columnOnlineOrderFlag = this.Columns["OnlineOrderFlag"];
			_columnSalesOrderNumber = this.Columns["SalesOrderNumber"];
			_columnPurchaseOrderNumber = this.Columns["PurchaseOrderNumber"];
			_columnAccountNumber = this.Columns["AccountNumber"];
			_columnCustomerId = this.Columns["CustomerId"];
			_columnSalesPersonId = this.Columns["SalesPersonId"];
			_columnTerritoryId = this.Columns["TerritoryId"];
			_columnBillToAddressId = this.Columns["BillToAddressId"];
			_columnShipToAddressId = this.Columns["ShipToAddressId"];
			_columnShipMethodId = this.Columns["ShipMethodId"];
			_columnCreditCardId = this.Columns["CreditCardId"];
			_columnCreditCardApprovalCode = this.Columns["CreditCardApprovalCode"];
			_columnCurrencyRateId = this.Columns["CurrencyRateId"];
			_columnSubTotal = this.Columns["SubTotal"];
			_columnTaxAmt = this.Columns["TaxAmt"];
			_columnFreight = this.Columns["Freight"];
			_columnTotalDue = this.Columns["TotalDue"];
			_columnComment = this.Columns["Comment"];
			_columnRowguid = this.Columns["Rowguid"];
			_columnModifiedDate = this.Columns["ModifiedDate"];
			_fields = EntityFieldsFactory.CreateTypedViewEntityFieldsObject(TypedViewType.SohTypedView);
			// __LLBLGENPRO_USER_CODE_REGION_START InitMembers
			// __LLBLGENPRO_USER_CODE_REGION_END
			
		}

		/// <summary>Clones this instance.</summary>
		/// <returns>A clone of this instance</returns>
		public override DataTable Clone() 
		{
			SohTypedView cloneToReturn = ((SohTypedView)(base.Clone()));
			cloneToReturn.InitMembers();
			return cloneToReturn;
		}
#if !CF			
		/// <summary>Creates a new instance of the DataTable class.</summary>
		/// <returns>a new instance of a datatable with this schema.</returns>
		protected override DataTable CreateInstance() 
		{
			return new SohTypedView();
		}
#endif

		#region Class Property Declarations
		/// <summary>The custom properties for this TypedView type.</summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public static Hashtable CustomProperties
		{
			get { return _customProperties;}
		}

		/// <summary>The custom properties for the type of this TypedView instance.</summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		[System.ComponentModel.Browsable(false)]
		public virtual Hashtable CustomPropertiesOfType
		{
			get { return SohTypedView.CustomProperties;}
		}

		/// <summary>The custom properties for the fields of this TypedView type. The returned Hashtable contains per fieldname a hashtable of name-value pairs. </summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public static Hashtable FieldsCustomProperties
		{
			get { return _fieldsCustomProperties;}
		}

		/// <summary>The custom properties for the fields of the type of this TypedView instance. The returned Hashtable contains per fieldname a hashtable of name-value pairs.</summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		[System.ComponentModel.Browsable(false)]
		public virtual Hashtable FieldsCustomPropertiesOfType
		{
			get { return SohTypedView.FieldsCustomProperties;}
		}

		/// <summary>Returns the column object belonging to the TypedView field SalesOrderId</summary>
		internal DataColumn SalesOrderIdColumn 
		{
			get { return _columnSalesOrderId; }
		}

		/// <summary>Returns the column object belonging to the TypedView field RevisionNumber</summary>
		internal DataColumn RevisionNumberColumn 
		{
			get { return _columnRevisionNumber; }
		}

		/// <summary>Returns the column object belonging to the TypedView field OrderDate</summary>
		internal DataColumn OrderDateColumn 
		{
			get { return _columnOrderDate; }
		}

		/// <summary>Returns the column object belonging to the TypedView field DueDate</summary>
		internal DataColumn DueDateColumn 
		{
			get { return _columnDueDate; }
		}

		/// <summary>Returns the column object belonging to the TypedView field ShipDate</summary>
		internal DataColumn ShipDateColumn 
		{
			get { return _columnShipDate; }
		}

		/// <summary>Returns the column object belonging to the TypedView field Status</summary>
		internal DataColumn StatusColumn 
		{
			get { return _columnStatus; }
		}

		/// <summary>Returns the column object belonging to the TypedView field OnlineOrderFlag</summary>
		internal DataColumn OnlineOrderFlagColumn 
		{
			get { return _columnOnlineOrderFlag; }
		}

		/// <summary>Returns the column object belonging to the TypedView field SalesOrderNumber</summary>
		internal DataColumn SalesOrderNumberColumn 
		{
			get { return _columnSalesOrderNumber; }
		}

		/// <summary>Returns the column object belonging to the TypedView field PurchaseOrderNumber</summary>
		internal DataColumn PurchaseOrderNumberColumn 
		{
			get { return _columnPurchaseOrderNumber; }
		}

		/// <summary>Returns the column object belonging to the TypedView field AccountNumber</summary>
		internal DataColumn AccountNumberColumn 
		{
			get { return _columnAccountNumber; }
		}

		/// <summary>Returns the column object belonging to the TypedView field CustomerId</summary>
		internal DataColumn CustomerIdColumn 
		{
			get { return _columnCustomerId; }
		}

		/// <summary>Returns the column object belonging to the TypedView field SalesPersonId</summary>
		internal DataColumn SalesPersonIdColumn 
		{
			get { return _columnSalesPersonId; }
		}

		/// <summary>Returns the column object belonging to the TypedView field TerritoryId</summary>
		internal DataColumn TerritoryIdColumn 
		{
			get { return _columnTerritoryId; }
		}

		/// <summary>Returns the column object belonging to the TypedView field BillToAddressId</summary>
		internal DataColumn BillToAddressIdColumn 
		{
			get { return _columnBillToAddressId; }
		}

		/// <summary>Returns the column object belonging to the TypedView field ShipToAddressId</summary>
		internal DataColumn ShipToAddressIdColumn 
		{
			get { return _columnShipToAddressId; }
		}

		/// <summary>Returns the column object belonging to the TypedView field ShipMethodId</summary>
		internal DataColumn ShipMethodIdColumn 
		{
			get { return _columnShipMethodId; }
		}

		/// <summary>Returns the column object belonging to the TypedView field CreditCardId</summary>
		internal DataColumn CreditCardIdColumn 
		{
			get { return _columnCreditCardId; }
		}

		/// <summary>Returns the column object belonging to the TypedView field CreditCardApprovalCode</summary>
		internal DataColumn CreditCardApprovalCodeColumn 
		{
			get { return _columnCreditCardApprovalCode; }
		}

		/// <summary>Returns the column object belonging to the TypedView field CurrencyRateId</summary>
		internal DataColumn CurrencyRateIdColumn 
		{
			get { return _columnCurrencyRateId; }
		}

		/// <summary>Returns the column object belonging to the TypedView field SubTotal</summary>
		internal DataColumn SubTotalColumn 
		{
			get { return _columnSubTotal; }
		}

		/// <summary>Returns the column object belonging to the TypedView field TaxAmt</summary>
		internal DataColumn TaxAmtColumn 
		{
			get { return _columnTaxAmt; }
		}

		/// <summary>Returns the column object belonging to the TypedView field Freight</summary>
		internal DataColumn FreightColumn 
		{
			get { return _columnFreight; }
		}

		/// <summary>Returns the column object belonging to the TypedView field TotalDue</summary>
		internal DataColumn TotalDueColumn 
		{
			get { return _columnTotalDue; }
		}

		/// <summary>Returns the column object belonging to the TypedView field Comment</summary>
		internal DataColumn CommentColumn 
		{
			get { return _columnComment; }
		}

		/// <summary>Returns the column object belonging to the TypedView field Rowguid</summary>
		internal DataColumn RowguidColumn 
		{
			get { return _columnRowguid; }
		}

		/// <summary>Returns the column object belonging to the TypedView field ModifiedDate</summary>
		internal DataColumn ModifiedDateColumn 
		{
			get { return _columnModifiedDate; }
		}

		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalColumnProperties
		// __LLBLGENPRO_USER_CODE_REGION_END
		
 		#endregion

		#region Custom TypedView code
		
		// __LLBLGENPRO_USER_CODE_REGION_START CustomTypedViewCode
		// __LLBLGENPRO_USER_CODE_REGION_END
		
		#endregion

		#region Included Code

		#endregion
	}

	/// <summary>Typed datarow for the typed datatable Soh</summary>
	public partial class SohRow : DataRow
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfacesRow
		// __LLBLGENPRO_USER_CODE_REGION_END
			
	{
		#region Class Member Declarations
		private SohTypedView	_parent;
		#endregion

		/// <summary>CTor</summary>
		/// <param name="rowBuilder">Row builder object to use when building this row</param>
		protected internal SohRow(DataRowBuilder rowBuilder) : base(rowBuilder) 
		{
			_parent = ((SohTypedView)(this.Table));
		}

		#region Class Property Declarations

		/// <summary>Gets / sets the value of the TypedView field SalesOrderId<br/><br/></summary>
		/// <remarks>Mapped on view field: "SalesOrderHeader"."SalesOrderID"<br/>
		/// View field characteristics (type, precision, scale, length): Int, 10, 0, 0</remarks>
		public System.Int32 SalesOrderId 
		{
			get { return IsSalesOrderIdNull() ? (System.Int32)TypeDefaultValue.GetDefaultValue(typeof(System.Int32)) : (System.Int32)this[_parent.SalesOrderIdColumn]; }
			set { this[_parent.SalesOrderIdColumn] = value; }
		}

		/// <summary>Returns true if the TypedView field SalesOrderId is NULL, false otherwise.</summary>
		public bool IsSalesOrderIdNull() 
		{
			return IsNull(_parent.SalesOrderIdColumn);
		}

		/// <summary>Sets the TypedView field SalesOrderId to NULL. Not recommended; a typed list should be used as a readonly object.</summary>
    	public void SetSalesOrderIdNull() 
		{
			this[_parent.SalesOrderIdColumn] = System.Convert.DBNull;
		}

		/// <summary>Gets / sets the value of the TypedView field RevisionNumber<br/><br/></summary>
		/// <remarks>Mapped on view field: "SalesOrderHeader"."RevisionNumber"<br/>
		/// View field characteristics (type, precision, scale, length): TinyInt, 3, 0, 0</remarks>
		public System.Byte RevisionNumber 
		{
			get { return IsRevisionNumberNull() ? (System.Byte)TypeDefaultValue.GetDefaultValue(typeof(System.Byte)) : (System.Byte)this[_parent.RevisionNumberColumn]; }
			set { this[_parent.RevisionNumberColumn] = value; }
		}

		/// <summary>Returns true if the TypedView field RevisionNumber is NULL, false otherwise.</summary>
		public bool IsRevisionNumberNull() 
		{
			return IsNull(_parent.RevisionNumberColumn);
		}

		/// <summary>Sets the TypedView field RevisionNumber to NULL. Not recommended; a typed list should be used as a readonly object.</summary>
    	public void SetRevisionNumberNull() 
		{
			this[_parent.RevisionNumberColumn] = System.Convert.DBNull;
		}

		/// <summary>Gets / sets the value of the TypedView field OrderDate<br/><br/></summary>
		/// <remarks>Mapped on view field: "SalesOrderHeader"."OrderDate"<br/>
		/// View field characteristics (type, precision, scale, length): DateTime, 0, 0, 0</remarks>
		public System.DateTime OrderDate 
		{
			get { return IsOrderDateNull() ? (System.DateTime)TypeDefaultValue.GetDefaultValue(typeof(System.DateTime)) : (System.DateTime)this[_parent.OrderDateColumn]; }
			set { this[_parent.OrderDateColumn] = value; }
		}

		/// <summary>Returns true if the TypedView field OrderDate is NULL, false otherwise.</summary>
		public bool IsOrderDateNull() 
		{
			return IsNull(_parent.OrderDateColumn);
		}

		/// <summary>Sets the TypedView field OrderDate to NULL. Not recommended; a typed list should be used as a readonly object.</summary>
    	public void SetOrderDateNull() 
		{
			this[_parent.OrderDateColumn] = System.Convert.DBNull;
		}

		/// <summary>Gets / sets the value of the TypedView field DueDate<br/><br/></summary>
		/// <remarks>Mapped on view field: "SalesOrderHeader"."DueDate"<br/>
		/// View field characteristics (type, precision, scale, length): DateTime, 0, 0, 0</remarks>
		public System.DateTime DueDate 
		{
			get { return IsDueDateNull() ? (System.DateTime)TypeDefaultValue.GetDefaultValue(typeof(System.DateTime)) : (System.DateTime)this[_parent.DueDateColumn]; }
			set { this[_parent.DueDateColumn] = value; }
		}

		/// <summary>Returns true if the TypedView field DueDate is NULL, false otherwise.</summary>
		public bool IsDueDateNull() 
		{
			return IsNull(_parent.DueDateColumn);
		}

		/// <summary>Sets the TypedView field DueDate to NULL. Not recommended; a typed list should be used as a readonly object.</summary>
    	public void SetDueDateNull() 
		{
			this[_parent.DueDateColumn] = System.Convert.DBNull;
		}

		/// <summary>Gets / sets the value of the TypedView field ShipDate<br/><br/></summary>
		/// <remarks>Mapped on view field: "SalesOrderHeader"."ShipDate"<br/>
		/// View field characteristics (type, precision, scale, length): DateTime, 0, 0, 0</remarks>
		public System.DateTime ShipDate 
		{
			get { return IsShipDateNull() ? (System.DateTime)TypeDefaultValue.GetDefaultValue(typeof(System.DateTime)) : (System.DateTime)this[_parent.ShipDateColumn]; }
			set { this[_parent.ShipDateColumn] = value; }
		}

		/// <summary>Returns true if the TypedView field ShipDate is NULL, false otherwise.</summary>
		public bool IsShipDateNull() 
		{
			return IsNull(_parent.ShipDateColumn);
		}

		/// <summary>Sets the TypedView field ShipDate to NULL. Not recommended; a typed list should be used as a readonly object.</summary>
    	public void SetShipDateNull() 
		{
			this[_parent.ShipDateColumn] = System.Convert.DBNull;
		}

		/// <summary>Gets / sets the value of the TypedView field Status<br/><br/></summary>
		/// <remarks>Mapped on view field: "SalesOrderHeader"."Status"<br/>
		/// View field characteristics (type, precision, scale, length): TinyInt, 3, 0, 0</remarks>
		public System.Byte Status 
		{
			get { return IsStatusNull() ? (System.Byte)TypeDefaultValue.GetDefaultValue(typeof(System.Byte)) : (System.Byte)this[_parent.StatusColumn]; }
			set { this[_parent.StatusColumn] = value; }
		}

		/// <summary>Returns true if the TypedView field Status is NULL, false otherwise.</summary>
		public bool IsStatusNull() 
		{
			return IsNull(_parent.StatusColumn);
		}

		/// <summary>Sets the TypedView field Status to NULL. Not recommended; a typed list should be used as a readonly object.</summary>
    	public void SetStatusNull() 
		{
			this[_parent.StatusColumn] = System.Convert.DBNull;
		}

		/// <summary>Gets / sets the value of the TypedView field OnlineOrderFlag<br/><br/></summary>
		/// <remarks>Mapped on view field: "SalesOrderHeader"."OnlineOrderFlag"<br/>
		/// View field characteristics (type, precision, scale, length): Bit, 0, 0, 0</remarks>
		public System.Boolean OnlineOrderFlag 
		{
			get { return IsOnlineOrderFlagNull() ? (System.Boolean)TypeDefaultValue.GetDefaultValue(typeof(System.Boolean)) : (System.Boolean)this[_parent.OnlineOrderFlagColumn]; }
			set { this[_parent.OnlineOrderFlagColumn] = value; }
		}

		/// <summary>Returns true if the TypedView field OnlineOrderFlag is NULL, false otherwise.</summary>
		public bool IsOnlineOrderFlagNull() 
		{
			return IsNull(_parent.OnlineOrderFlagColumn);
		}

		/// <summary>Sets the TypedView field OnlineOrderFlag to NULL. Not recommended; a typed list should be used as a readonly object.</summary>
    	public void SetOnlineOrderFlagNull() 
		{
			this[_parent.OnlineOrderFlagColumn] = System.Convert.DBNull;
		}

		/// <summary>Gets / sets the value of the TypedView field SalesOrderNumber<br/><br/></summary>
		/// <remarks>Mapped on view field: "SalesOrderHeader"."SalesOrderNumber"<br/>
		/// View field characteristics (type, precision, scale, length): NVarChar, 0, 0, 25</remarks>
		public System.String SalesOrderNumber 
		{
			get { return IsSalesOrderNumberNull() ? (System.String)TypeDefaultValue.GetDefaultValue(typeof(System.String)) : (System.String)this[_parent.SalesOrderNumberColumn]; }
			set { this[_parent.SalesOrderNumberColumn] = value; }
		}

		/// <summary>Returns true if the TypedView field SalesOrderNumber is NULL, false otherwise.</summary>
		public bool IsSalesOrderNumberNull() 
		{
			return IsNull(_parent.SalesOrderNumberColumn);
		}

		/// <summary>Sets the TypedView field SalesOrderNumber to NULL. Not recommended; a typed list should be used as a readonly object.</summary>
    	public void SetSalesOrderNumberNull() 
		{
			this[_parent.SalesOrderNumberColumn] = System.Convert.DBNull;
		}

		/// <summary>Gets / sets the value of the TypedView field PurchaseOrderNumber<br/><br/></summary>
		/// <remarks>Mapped on view field: "SalesOrderHeader"."PurchaseOrderNumber"<br/>
		/// View field characteristics (type, precision, scale, length): NVarChar, 0, 0, 25</remarks>
		public System.String PurchaseOrderNumber 
		{
			get { return IsPurchaseOrderNumberNull() ? (System.String)TypeDefaultValue.GetDefaultValue(typeof(System.String)) : (System.String)this[_parent.PurchaseOrderNumberColumn]; }
			set { this[_parent.PurchaseOrderNumberColumn] = value; }
		}

		/// <summary>Returns true if the TypedView field PurchaseOrderNumber is NULL, false otherwise.</summary>
		public bool IsPurchaseOrderNumberNull() 
		{
			return IsNull(_parent.PurchaseOrderNumberColumn);
		}

		/// <summary>Sets the TypedView field PurchaseOrderNumber to NULL. Not recommended; a typed list should be used as a readonly object.</summary>
    	public void SetPurchaseOrderNumberNull() 
		{
			this[_parent.PurchaseOrderNumberColumn] = System.Convert.DBNull;
		}

		/// <summary>Gets / sets the value of the TypedView field AccountNumber<br/><br/></summary>
		/// <remarks>Mapped on view field: "SalesOrderHeader"."AccountNumber"<br/>
		/// View field characteristics (type, precision, scale, length): NVarChar, 0, 0, 15</remarks>
		public System.String AccountNumber 
		{
			get { return IsAccountNumberNull() ? (System.String)TypeDefaultValue.GetDefaultValue(typeof(System.String)) : (System.String)this[_parent.AccountNumberColumn]; }
			set { this[_parent.AccountNumberColumn] = value; }
		}

		/// <summary>Returns true if the TypedView field AccountNumber is NULL, false otherwise.</summary>
		public bool IsAccountNumberNull() 
		{
			return IsNull(_parent.AccountNumberColumn);
		}

		/// <summary>Sets the TypedView field AccountNumber to NULL. Not recommended; a typed list should be used as a readonly object.</summary>
    	public void SetAccountNumberNull() 
		{
			this[_parent.AccountNumberColumn] = System.Convert.DBNull;
		}

		/// <summary>Gets / sets the value of the TypedView field CustomerId<br/><br/></summary>
		/// <remarks>Mapped on view field: "SalesOrderHeader"."CustomerID"<br/>
		/// View field characteristics (type, precision, scale, length): Int, 10, 0, 0</remarks>
		public System.Int32 CustomerId 
		{
			get { return IsCustomerIdNull() ? (System.Int32)TypeDefaultValue.GetDefaultValue(typeof(System.Int32)) : (System.Int32)this[_parent.CustomerIdColumn]; }
			set { this[_parent.CustomerIdColumn] = value; }
		}

		/// <summary>Returns true if the TypedView field CustomerId is NULL, false otherwise.</summary>
		public bool IsCustomerIdNull() 
		{
			return IsNull(_parent.CustomerIdColumn);
		}

		/// <summary>Sets the TypedView field CustomerId to NULL. Not recommended; a typed list should be used as a readonly object.</summary>
    	public void SetCustomerIdNull() 
		{
			this[_parent.CustomerIdColumn] = System.Convert.DBNull;
		}

		/// <summary>Gets / sets the value of the TypedView field SalesPersonId<br/><br/></summary>
		/// <remarks>Mapped on view field: "SalesOrderHeader"."SalesPersonID"<br/>
		/// View field characteristics (type, precision, scale, length): Int, 10, 0, 0</remarks>
		public System.Int32 SalesPersonId 
		{
			get { return IsSalesPersonIdNull() ? (System.Int32)TypeDefaultValue.GetDefaultValue(typeof(System.Int32)) : (System.Int32)this[_parent.SalesPersonIdColumn]; }
			set { this[_parent.SalesPersonIdColumn] = value; }
		}

		/// <summary>Returns true if the TypedView field SalesPersonId is NULL, false otherwise.</summary>
		public bool IsSalesPersonIdNull() 
		{
			return IsNull(_parent.SalesPersonIdColumn);
		}

		/// <summary>Sets the TypedView field SalesPersonId to NULL. Not recommended; a typed list should be used as a readonly object.</summary>
    	public void SetSalesPersonIdNull() 
		{
			this[_parent.SalesPersonIdColumn] = System.Convert.DBNull;
		}

		/// <summary>Gets / sets the value of the TypedView field TerritoryId<br/><br/></summary>
		/// <remarks>Mapped on view field: "SalesOrderHeader"."TerritoryID"<br/>
		/// View field characteristics (type, precision, scale, length): Int, 10, 0, 0</remarks>
		public System.Int32 TerritoryId 
		{
			get { return IsTerritoryIdNull() ? (System.Int32)TypeDefaultValue.GetDefaultValue(typeof(System.Int32)) : (System.Int32)this[_parent.TerritoryIdColumn]; }
			set { this[_parent.TerritoryIdColumn] = value; }
		}

		/// <summary>Returns true if the TypedView field TerritoryId is NULL, false otherwise.</summary>
		public bool IsTerritoryIdNull() 
		{
			return IsNull(_parent.TerritoryIdColumn);
		}

		/// <summary>Sets the TypedView field TerritoryId to NULL. Not recommended; a typed list should be used as a readonly object.</summary>
    	public void SetTerritoryIdNull() 
		{
			this[_parent.TerritoryIdColumn] = System.Convert.DBNull;
		}

		/// <summary>Gets / sets the value of the TypedView field BillToAddressId<br/><br/></summary>
		/// <remarks>Mapped on view field: "SalesOrderHeader"."BillToAddressID"<br/>
		/// View field characteristics (type, precision, scale, length): Int, 10, 0, 0</remarks>
		public System.Int32 BillToAddressId 
		{
			get { return IsBillToAddressIdNull() ? (System.Int32)TypeDefaultValue.GetDefaultValue(typeof(System.Int32)) : (System.Int32)this[_parent.BillToAddressIdColumn]; }
			set { this[_parent.BillToAddressIdColumn] = value; }
		}

		/// <summary>Returns true if the TypedView field BillToAddressId is NULL, false otherwise.</summary>
		public bool IsBillToAddressIdNull() 
		{
			return IsNull(_parent.BillToAddressIdColumn);
		}

		/// <summary>Sets the TypedView field BillToAddressId to NULL. Not recommended; a typed list should be used as a readonly object.</summary>
    	public void SetBillToAddressIdNull() 
		{
			this[_parent.BillToAddressIdColumn] = System.Convert.DBNull;
		}

		/// <summary>Gets / sets the value of the TypedView field ShipToAddressId<br/><br/></summary>
		/// <remarks>Mapped on view field: "SalesOrderHeader"."ShipToAddressID"<br/>
		/// View field characteristics (type, precision, scale, length): Int, 10, 0, 0</remarks>
		public System.Int32 ShipToAddressId 
		{
			get { return IsShipToAddressIdNull() ? (System.Int32)TypeDefaultValue.GetDefaultValue(typeof(System.Int32)) : (System.Int32)this[_parent.ShipToAddressIdColumn]; }
			set { this[_parent.ShipToAddressIdColumn] = value; }
		}

		/// <summary>Returns true if the TypedView field ShipToAddressId is NULL, false otherwise.</summary>
		public bool IsShipToAddressIdNull() 
		{
			return IsNull(_parent.ShipToAddressIdColumn);
		}

		/// <summary>Sets the TypedView field ShipToAddressId to NULL. Not recommended; a typed list should be used as a readonly object.</summary>
    	public void SetShipToAddressIdNull() 
		{
			this[_parent.ShipToAddressIdColumn] = System.Convert.DBNull;
		}

		/// <summary>Gets / sets the value of the TypedView field ShipMethodId<br/><br/></summary>
		/// <remarks>Mapped on view field: "SalesOrderHeader"."ShipMethodID"<br/>
		/// View field characteristics (type, precision, scale, length): Int, 10, 0, 0</remarks>
		public System.Int32 ShipMethodId 
		{
			get { return IsShipMethodIdNull() ? (System.Int32)TypeDefaultValue.GetDefaultValue(typeof(System.Int32)) : (System.Int32)this[_parent.ShipMethodIdColumn]; }
			set { this[_parent.ShipMethodIdColumn] = value; }
		}

		/// <summary>Returns true if the TypedView field ShipMethodId is NULL, false otherwise.</summary>
		public bool IsShipMethodIdNull() 
		{
			return IsNull(_parent.ShipMethodIdColumn);
		}

		/// <summary>Sets the TypedView field ShipMethodId to NULL. Not recommended; a typed list should be used as a readonly object.</summary>
    	public void SetShipMethodIdNull() 
		{
			this[_parent.ShipMethodIdColumn] = System.Convert.DBNull;
		}

		/// <summary>Gets / sets the value of the TypedView field CreditCardId<br/><br/></summary>
		/// <remarks>Mapped on view field: "SalesOrderHeader"."CreditCardID"<br/>
		/// View field characteristics (type, precision, scale, length): Int, 10, 0, 0</remarks>
		public System.Int32 CreditCardId 
		{
			get { return IsCreditCardIdNull() ? (System.Int32)TypeDefaultValue.GetDefaultValue(typeof(System.Int32)) : (System.Int32)this[_parent.CreditCardIdColumn]; }
			set { this[_parent.CreditCardIdColumn] = value; }
		}

		/// <summary>Returns true if the TypedView field CreditCardId is NULL, false otherwise.</summary>
		public bool IsCreditCardIdNull() 
		{
			return IsNull(_parent.CreditCardIdColumn);
		}

		/// <summary>Sets the TypedView field CreditCardId to NULL. Not recommended; a typed list should be used as a readonly object.</summary>
    	public void SetCreditCardIdNull() 
		{
			this[_parent.CreditCardIdColumn] = System.Convert.DBNull;
		}

		/// <summary>Gets / sets the value of the TypedView field CreditCardApprovalCode<br/><br/></summary>
		/// <remarks>Mapped on view field: "SalesOrderHeader"."CreditCardApprovalCode"<br/>
		/// View field characteristics (type, precision, scale, length): VarChar, 0, 0, 15</remarks>
		public System.String CreditCardApprovalCode 
		{
			get { return IsCreditCardApprovalCodeNull() ? (System.String)TypeDefaultValue.GetDefaultValue(typeof(System.String)) : (System.String)this[_parent.CreditCardApprovalCodeColumn]; }
			set { this[_parent.CreditCardApprovalCodeColumn] = value; }
		}

		/// <summary>Returns true if the TypedView field CreditCardApprovalCode is NULL, false otherwise.</summary>
		public bool IsCreditCardApprovalCodeNull() 
		{
			return IsNull(_parent.CreditCardApprovalCodeColumn);
		}

		/// <summary>Sets the TypedView field CreditCardApprovalCode to NULL. Not recommended; a typed list should be used as a readonly object.</summary>
    	public void SetCreditCardApprovalCodeNull() 
		{
			this[_parent.CreditCardApprovalCodeColumn] = System.Convert.DBNull;
		}

		/// <summary>Gets / sets the value of the TypedView field CurrencyRateId<br/><br/></summary>
		/// <remarks>Mapped on view field: "SalesOrderHeader"."CurrencyRateID"<br/>
		/// View field characteristics (type, precision, scale, length): Int, 10, 0, 0</remarks>
		public System.Int32 CurrencyRateId 
		{
			get { return IsCurrencyRateIdNull() ? (System.Int32)TypeDefaultValue.GetDefaultValue(typeof(System.Int32)) : (System.Int32)this[_parent.CurrencyRateIdColumn]; }
			set { this[_parent.CurrencyRateIdColumn] = value; }
		}

		/// <summary>Returns true if the TypedView field CurrencyRateId is NULL, false otherwise.</summary>
		public bool IsCurrencyRateIdNull() 
		{
			return IsNull(_parent.CurrencyRateIdColumn);
		}

		/// <summary>Sets the TypedView field CurrencyRateId to NULL. Not recommended; a typed list should be used as a readonly object.</summary>
    	public void SetCurrencyRateIdNull() 
		{
			this[_parent.CurrencyRateIdColumn] = System.Convert.DBNull;
		}

		/// <summary>Gets / sets the value of the TypedView field SubTotal<br/><br/></summary>
		/// <remarks>Mapped on view field: "SalesOrderHeader"."SubTotal"<br/>
		/// View field characteristics (type, precision, scale, length): Money, 19, 4, 0</remarks>
		public System.Decimal SubTotal 
		{
			get { return IsSubTotalNull() ? (System.Decimal)TypeDefaultValue.GetDefaultValue(typeof(System.Decimal)) : (System.Decimal)this[_parent.SubTotalColumn]; }
			set { this[_parent.SubTotalColumn] = value; }
		}

		/// <summary>Returns true if the TypedView field SubTotal is NULL, false otherwise.</summary>
		public bool IsSubTotalNull() 
		{
			return IsNull(_parent.SubTotalColumn);
		}

		/// <summary>Sets the TypedView field SubTotal to NULL. Not recommended; a typed list should be used as a readonly object.</summary>
    	public void SetSubTotalNull() 
		{
			this[_parent.SubTotalColumn] = System.Convert.DBNull;
		}

		/// <summary>Gets / sets the value of the TypedView field TaxAmt<br/><br/></summary>
		/// <remarks>Mapped on view field: "SalesOrderHeader"."TaxAmt"<br/>
		/// View field characteristics (type, precision, scale, length): Money, 19, 4, 0</remarks>
		public System.Decimal TaxAmt 
		{
			get { return IsTaxAmtNull() ? (System.Decimal)TypeDefaultValue.GetDefaultValue(typeof(System.Decimal)) : (System.Decimal)this[_parent.TaxAmtColumn]; }
			set { this[_parent.TaxAmtColumn] = value; }
		}

		/// <summary>Returns true if the TypedView field TaxAmt is NULL, false otherwise.</summary>
		public bool IsTaxAmtNull() 
		{
			return IsNull(_parent.TaxAmtColumn);
		}

		/// <summary>Sets the TypedView field TaxAmt to NULL. Not recommended; a typed list should be used as a readonly object.</summary>
    	public void SetTaxAmtNull() 
		{
			this[_parent.TaxAmtColumn] = System.Convert.DBNull;
		}

		/// <summary>Gets / sets the value of the TypedView field Freight<br/><br/></summary>
		/// <remarks>Mapped on view field: "SalesOrderHeader"."Freight"<br/>
		/// View field characteristics (type, precision, scale, length): Money, 19, 4, 0</remarks>
		public System.Decimal Freight 
		{
			get { return IsFreightNull() ? (System.Decimal)TypeDefaultValue.GetDefaultValue(typeof(System.Decimal)) : (System.Decimal)this[_parent.FreightColumn]; }
			set { this[_parent.FreightColumn] = value; }
		}

		/// <summary>Returns true if the TypedView field Freight is NULL, false otherwise.</summary>
		public bool IsFreightNull() 
		{
			return IsNull(_parent.FreightColumn);
		}

		/// <summary>Sets the TypedView field Freight to NULL. Not recommended; a typed list should be used as a readonly object.</summary>
    	public void SetFreightNull() 
		{
			this[_parent.FreightColumn] = System.Convert.DBNull;
		}

		/// <summary>Gets / sets the value of the TypedView field TotalDue<br/><br/></summary>
		/// <remarks>Mapped on view field: "SalesOrderHeader"."TotalDue"<br/>
		/// View field characteristics (type, precision, scale, length): Money, 19, 4, 0</remarks>
		public System.Decimal TotalDue 
		{
			get { return IsTotalDueNull() ? (System.Decimal)TypeDefaultValue.GetDefaultValue(typeof(System.Decimal)) : (System.Decimal)this[_parent.TotalDueColumn]; }
			set { this[_parent.TotalDueColumn] = value; }
		}

		/// <summary>Returns true if the TypedView field TotalDue is NULL, false otherwise.</summary>
		public bool IsTotalDueNull() 
		{
			return IsNull(_parent.TotalDueColumn);
		}

		/// <summary>Sets the TypedView field TotalDue to NULL. Not recommended; a typed list should be used as a readonly object.</summary>
    	public void SetTotalDueNull() 
		{
			this[_parent.TotalDueColumn] = System.Convert.DBNull;
		}

		/// <summary>Gets / sets the value of the TypedView field Comment<br/><br/></summary>
		/// <remarks>Mapped on view field: "SalesOrderHeader"."Comment"<br/>
		/// View field characteristics (type, precision, scale, length): NVarChar, 0, 0, 128</remarks>
		public System.String Comment 
		{
			get { return IsCommentNull() ? (System.String)TypeDefaultValue.GetDefaultValue(typeof(System.String)) : (System.String)this[_parent.CommentColumn]; }
			set { this[_parent.CommentColumn] = value; }
		}

		/// <summary>Returns true if the TypedView field Comment is NULL, false otherwise.</summary>
		public bool IsCommentNull() 
		{
			return IsNull(_parent.CommentColumn);
		}

		/// <summary>Sets the TypedView field Comment to NULL. Not recommended; a typed list should be used as a readonly object.</summary>
    	public void SetCommentNull() 
		{
			this[_parent.CommentColumn] = System.Convert.DBNull;
		}

		/// <summary>Gets / sets the value of the TypedView field Rowguid<br/><br/></summary>
		/// <remarks>Mapped on view field: "SalesOrderHeader"."rowguid"<br/>
		/// View field characteristics (type, precision, scale, length): UniqueIdentifier, 0, 0, 0</remarks>
		public System.Guid Rowguid 
		{
			get { return IsRowguidNull() ? (System.Guid)TypeDefaultValue.GetDefaultValue(typeof(System.Guid)) : (System.Guid)this[_parent.RowguidColumn]; }
			set { this[_parent.RowguidColumn] = value; }
		}

		/// <summary>Returns true if the TypedView field Rowguid is NULL, false otherwise.</summary>
		public bool IsRowguidNull() 
		{
			return IsNull(_parent.RowguidColumn);
		}

		/// <summary>Sets the TypedView field Rowguid to NULL. Not recommended; a typed list should be used as a readonly object.</summary>
    	public void SetRowguidNull() 
		{
			this[_parent.RowguidColumn] = System.Convert.DBNull;
		}

		/// <summary>Gets / sets the value of the TypedView field ModifiedDate<br/><br/></summary>
		/// <remarks>Mapped on view field: "SalesOrderHeader"."ModifiedDate"<br/>
		/// View field characteristics (type, precision, scale, length): DateTime, 0, 0, 0</remarks>
		public System.DateTime ModifiedDate 
		{
			get { return IsModifiedDateNull() ? (System.DateTime)TypeDefaultValue.GetDefaultValue(typeof(System.DateTime)) : (System.DateTime)this[_parent.ModifiedDateColumn]; }
			set { this[_parent.ModifiedDateColumn] = value; }
		}

		/// <summary>Returns true if the TypedView field ModifiedDate is NULL, false otherwise.</summary>
		public bool IsModifiedDateNull() 
		{
			return IsNull(_parent.ModifiedDateColumn);
		}

		/// <summary>Sets the TypedView field ModifiedDate to NULL. Not recommended; a typed list should be used as a readonly object.</summary>
    	public void SetModifiedDateNull() 
		{
			this[_parent.ModifiedDateColumn] = System.Convert.DBNull;
		}
		#endregion
		
		#region Custom Typed View Row Code
		
		// __LLBLGENPRO_USER_CODE_REGION_START CustomTypedViewRowCode
		// __LLBLGENPRO_USER_CODE_REGION_END
		
		#endregion
		
		#region Included Row Code

		#endregion	
	}
}
