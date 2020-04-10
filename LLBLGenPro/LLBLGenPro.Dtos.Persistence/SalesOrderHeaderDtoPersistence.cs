﻿//------------------------------------------------------------------------------
// <auto-generated>This code was generated by LLBLGen Pro v5.7.</auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SD.LLBLGen.Pro.QuerySpec;
using AdventureWorks.Dal.Adapter.HelperClasses;

namespace LLBLGenPro.Dtos.Persistence
{
	/// <summary>Static class for (extension) methods for fetching and projecting instances of LLBLGenPro.Dtos.DtoClasses.SalesOrderHeaderDto from the entity model.</summary>
	public static partial class SalesOrderHeaderPersistence
	{
		private static readonly System.Linq.Expressions.Expression<Func<AdventureWorks.Dal.Adapter.EntityClasses.SalesOrderHeaderEntity, LLBLGenPro.Dtos.DtoClasses.SalesOrderHeaderDto>> _projectorExpression = CreateProjectionFunc();
		private static readonly Func<AdventureWorks.Dal.Adapter.EntityClasses.SalesOrderHeaderEntity, LLBLGenPro.Dtos.DtoClasses.SalesOrderHeaderDto> _compiledProjector = CreateProjectionFunc().Compile();
	
		/// <summary>Empty static ctor for triggering initialization of static members in a thread-safe manner</summary>
		static SalesOrderHeaderPersistence() { }
	
		/// <summary>Extension method which produces a projection to LLBLGenPro.Dtos.DtoClasses.SalesOrderHeaderDto which instances are projected from the 
		/// results of the specified baseQuery, which returns AdventureWorks.Dal.Adapter.EntityClasses.SalesOrderHeaderEntity instances, the root entity of the derived element returned by this query.</summary>
		/// <param name="baseQuery">The base query to project the derived element instances from.</param>
		/// <returns>IQueryable to retrieve LLBLGenPro.Dtos.DtoClasses.SalesOrderHeaderDto instances</returns>
		public static IQueryable<LLBLGenPro.Dtos.DtoClasses.SalesOrderHeaderDto> ProjectToSalesOrderHeaderDto(this IQueryable<AdventureWorks.Dal.Adapter.EntityClasses.SalesOrderHeaderEntity> baseQuery)
		{
			return baseQuery.Select(_projectorExpression);
		}

		/// <summary>Extension method which produces a projection to LLBLGenPro.Dtos.DtoClasses.SalesOrderHeaderDto which instances are projected from the 
		/// results of the specified baseQuery using QuerySpec, which returns AdventureWorks.Dal.Adapter.EntityClasses.SalesOrderHeaderEntity instances, the root entity of the derived element returned by this query.</summary>
		/// <param name="baseQuery">The base query to project the derived element instances from.</param>
		/// <param name="qf">The query factory used to create baseQuery.</param>
		/// <returns>DynamicQuery to retrieve LLBLGenPro.Dtos.DtoClasses.SalesOrderHeaderDto instances</returns>
		public static DynamicQuery<LLBLGenPro.Dtos.DtoClasses.SalesOrderHeaderDto> ProjectToSalesOrderHeaderDto(this EntityQuery<AdventureWorks.Dal.Adapter.EntityClasses.SalesOrderHeaderEntity> baseQuery, AdventureWorks.Dal.Adapter.FactoryClasses.QueryFactory qf)
		{
			return qf.Create()
				.From(baseQuery.Select(Projection.Full).As("__BQ")
					.InnerJoin(qf.Customer.As("__L0_0")).On(SalesOrderHeaderFields.CustomerId.Source("__BQ").Equal(CustomerFields.CustomerId.Source("__L0_0"))))
				.Select(() => new LLBLGenPro.Dtos.DtoClasses.SalesOrderHeaderDto()
				{
					AccountNumber = SalesOrderHeaderFields.AccountNumber.Source("__BQ").ToValue<System.String>(),
					BillToAddressId = SalesOrderHeaderFields.BillToAddressId.Source("__BQ").ToValue<System.Int32>(),
					Comment = SalesOrderHeaderFields.Comment.Source("__BQ").ToValue<System.String>(),
					CreditCardApprovalCode = SalesOrderHeaderFields.CreditCardApprovalCode.Source("__BQ").ToValue<System.String>(),
					CreditCardId = SalesOrderHeaderFields.CreditCardId.Source("__BQ").ToValue<Nullable<System.Int32>>(),
					CurrencyRateId = SalesOrderHeaderFields.CurrencyRateId.Source("__BQ").ToValue<Nullable<System.Int32>>(),
					Customer = new LLBLGenPro.Dtos.DtoClasses.SalesOrderHeaderDtoTypes.CustomerDto()
						{
							AccountNumber = CustomerFields.AccountNumber.As("AccountNumber1").Source("__L0_0").ToValue<System.String>(),
							CustomerId = CustomerFields.CustomerId.Source("__L0_0").ToValue<System.Int32>(),
							ModifiedDate = CustomerFields.ModifiedDate.Source("__L0_0").ToValue<System.DateTime>(),
							PersonId = CustomerFields.PersonId.Source("__L0_0").ToValue<Nullable<System.Int32>>(),
							Rowguid = CustomerFields.Rowguid.Source("__L0_0").ToValue<System.Guid>(),
							StoreId = CustomerFields.StoreId.Source("__L0_0").ToValue<Nullable<System.Int32>>(),
							TerritoryId = CustomerFields.TerritoryId.Source("__L0_0").ToValue<Nullable<System.Int32>>(),
						},
					CustomerId = SalesOrderHeaderFields.CustomerId.As("CustomerId1").Source("__BQ").ToValue<System.Int32>(),
					DueDate = SalesOrderHeaderFields.DueDate.Source("__BQ").ToValue<System.DateTime>(),
					Freight = SalesOrderHeaderFields.Freight.Source("__BQ").ToValue<System.Decimal>(),
					ModifiedDate = SalesOrderHeaderFields.ModifiedDate.As("ModifiedDate1").Source("__BQ").ToValue<System.DateTime>(),
					OnlineOrderFlag = SalesOrderHeaderFields.OnlineOrderFlag.Source("__BQ").ToValue<System.Boolean>(),
					OrderDate = SalesOrderHeaderFields.OrderDate.Source("__BQ").ToValue<System.DateTime>(),
					PurchaseOrderNumber = SalesOrderHeaderFields.PurchaseOrderNumber.Source("__BQ").ToValue<System.String>(),
					RevisionNumber = SalesOrderHeaderFields.RevisionNumber.Source("__BQ").ToValue<System.Byte>(),
					Rowguid = SalesOrderHeaderFields.Rowguid.As("Rowguid1").Source("__BQ").ToValue<System.Guid>(),
					SalesOrderDetails = (List<LLBLGenPro.Dtos.DtoClasses.SalesOrderHeaderDtoTypes.SalesOrderDetailDto>)qf.SalesOrderDetail.TargetAs("__L1_0")
						.CorrelatedOver(SalesOrderHeaderFields.SalesOrderId.Source("__BQ").Equal(SalesOrderDetailFields.SalesOrderId.Source("__L1_0")))
						.Select(() => new LLBLGenPro.Dtos.DtoClasses.SalesOrderHeaderDtoTypes.SalesOrderDetailDto()
						{
							CarrierTrackingNumber = SalesOrderDetailFields.CarrierTrackingNumber.Source("__L1_0").ToValue<System.String>(),
							LineTotal = SalesOrderDetailFields.LineTotal.Source("__L1_0").ToValue<System.Decimal>(),
							ModifiedDate = SalesOrderDetailFields.ModifiedDate.Source("__L1_0").ToValue<System.DateTime>(),
							OrderQty = SalesOrderDetailFields.OrderQty.Source("__L1_0").ToValue<System.Int16>(),
							ProductId = SalesOrderDetailFields.ProductId.Source("__L1_0").ToValue<System.Int32>(),
							Rowguid = SalesOrderDetailFields.Rowguid.Source("__L1_0").ToValue<System.Guid>(),
							SalesOrderDetailId = SalesOrderDetailFields.SalesOrderDetailId.Source("__L1_0").ToValue<System.Int32>(),
							SalesOrderId = SalesOrderDetailFields.SalesOrderId.Source("__L1_0").ToValue<System.Int32>(),
							SpecialOfferId = SalesOrderDetailFields.SpecialOfferId.Source("__L1_0").ToValue<System.Int32>(),
							UnitPrice = SalesOrderDetailFields.UnitPrice.Source("__L1_0").ToValue<System.Decimal>(),
							UnitPriceDiscount = SalesOrderDetailFields.UnitPriceDiscount.Source("__L1_0").ToValue<System.Decimal>(),
						}).ToResultset(),
					SalesOrderId = SalesOrderHeaderFields.SalesOrderId.Source("__BQ").ToValue<System.Int32>(),
					SalesOrderNumber = SalesOrderHeaderFields.SalesOrderNumber.Source("__BQ").ToValue<System.String>(),
					SalesPersonId = SalesOrderHeaderFields.SalesPersonId.Source("__BQ").ToValue<Nullable<System.Int32>>(),
					ShipDate = SalesOrderHeaderFields.ShipDate.Source("__BQ").ToValue<Nullable<System.DateTime>>(),
					ShipMethodId = SalesOrderHeaderFields.ShipMethodId.Source("__BQ").ToValue<System.Int32>(),
					ShipToAddressId = SalesOrderHeaderFields.ShipToAddressId.Source("__BQ").ToValue<System.Int32>(),
					Status = SalesOrderHeaderFields.Status.Source("__BQ").ToValue<System.Byte>(),
					SubTotal = SalesOrderHeaderFields.SubTotal.Source("__BQ").ToValue<System.Decimal>(),
					TaxAmt = SalesOrderHeaderFields.TaxAmt.Source("__BQ").ToValue<System.Decimal>(),
					TerritoryId = SalesOrderHeaderFields.TerritoryId.As("TerritoryId1").Source("__BQ").ToValue<Nullable<System.Int32>>(),
					TotalDue = SalesOrderHeaderFields.TotalDue.Source("__BQ").ToValue<System.Decimal>(),
	// __LLBLGENPRO_USER_CODE_REGION_START ProjectionRegionQS_SalesOrderHeader 
	// __LLBLGENPRO_USER_CODE_REGION_END 
				});
		}

		/// <summary>Extension method which produces a projection to LLBLGenPro.Dtos.DtoClasses.SalesOrderHeaderDto which instances are projected from the
		/// AdventureWorks.Dal.Adapter.EntityClasses.SalesOrderHeaderEntity entity instance specified, the root entity of the derived element returned by this method.</summary>
		/// <param name="entity">The entity to project from.</param>
		/// <returns>AdventureWorks.Dal.Adapter.EntityClasses.SalesOrderHeaderEntity instance created from the specified entity instance</returns>
		public static LLBLGenPro.Dtos.DtoClasses.SalesOrderHeaderDto ProjectToSalesOrderHeaderDto(this AdventureWorks.Dal.Adapter.EntityClasses.SalesOrderHeaderEntity entity)
		{
			return _compiledProjector(entity);
		}
		
		private static System.Linq.Expressions.Expression<Func<AdventureWorks.Dal.Adapter.EntityClasses.SalesOrderHeaderEntity, LLBLGenPro.Dtos.DtoClasses.SalesOrderHeaderDto>> CreateProjectionFunc()
		{
			return p__0 => new LLBLGenPro.Dtos.DtoClasses.SalesOrderHeaderDto()
			{
				AccountNumber = p__0.AccountNumber,
				BillToAddressId = p__0.BillToAddressId,
				Comment = p__0.Comment,
				CreditCardApprovalCode = p__0.CreditCardApprovalCode,
				CreditCardId = p__0.CreditCardId,
				CurrencyRateId = p__0.CurrencyRateId,
				Customer = new LLBLGenPro.Dtos.DtoClasses.SalesOrderHeaderDtoTypes.CustomerDto()
				{
					AccountNumber = p__0.Customer.AccountNumber,
					CustomerId = p__0.Customer.CustomerId,
					ModifiedDate = p__0.Customer.ModifiedDate,
					PersonId = p__0.Customer.PersonId,
					Rowguid = p__0.Customer.Rowguid,
					StoreId = p__0.Customer.StoreId,
					TerritoryId = p__0.Customer.TerritoryId,
				},
				CustomerId = p__0.CustomerId,
				DueDate = p__0.DueDate,
				Freight = p__0.Freight,
				ModifiedDate = p__0.ModifiedDate,
				OnlineOrderFlag = p__0.OnlineOrderFlag,
				OrderDate = p__0.OrderDate,
				PurchaseOrderNumber = p__0.PurchaseOrderNumber,
				RevisionNumber = p__0.RevisionNumber,
				Rowguid = p__0.Rowguid,
				SalesOrderDetails = p__0.SalesOrderDetails.Select(p__1 => new LLBLGenPro.Dtos.DtoClasses.SalesOrderHeaderDtoTypes.SalesOrderDetailDto()
				{
					CarrierTrackingNumber = p__1.CarrierTrackingNumber,
					LineTotal = p__1.LineTotal,
					ModifiedDate = p__1.ModifiedDate,
					OrderQty = p__1.OrderQty,
					ProductId = p__1.ProductId,
					Rowguid = p__1.Rowguid,
					SalesOrderDetailId = p__1.SalesOrderDetailId,
					SalesOrderId = p__1.SalesOrderId,
					SpecialOfferId = p__1.SpecialOfferId,
					UnitPrice = p__1.UnitPrice,
					UnitPriceDiscount = p__1.UnitPriceDiscount,
				}).ToList(),
				SalesOrderId = p__0.SalesOrderId,
				SalesOrderNumber = p__0.SalesOrderNumber,
				SalesPersonId = p__0.SalesPersonId,
				ShipDate = p__0.ShipDate,
				ShipMethodId = p__0.ShipMethodId,
				ShipToAddressId = p__0.ShipToAddressId,
				Status = p__0.Status,
				SubTotal = p__0.SubTotal,
				TaxAmt = p__0.TaxAmt,
				TerritoryId = p__0.TerritoryId,
				TotalDue = p__0.TotalDue,
	// __LLBLGENPRO_USER_CODE_REGION_START ProjectionRegion_SalesOrderHeader 
	// __LLBLGENPRO_USER_CODE_REGION_END 
			};
		}
	}
}
