﻿//------------------------------------------------------------------------------
// <auto-generated>This code was generated by LLBLGen Pro v5.5.</auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace EFCore.Bencher.EntityClasses
{
	/// <summary>Class which represents the entity 'Illustration'.</summary>
	public partial class Illustration : CommonEntityBase
	{
		/// <summary>Method called from the constructor</summary>
		partial void OnCreated();

		/// <summary>Initializes a new instance of the <see cref="Illustration"/> class.</summary>
		public Illustration() : base()
		{
			this.ProductModelIllustrations = new List<ProductModelIllustration>();
			OnCreated();
		}

		/// <summary>Gets or sets the Diagram field. </summary>
		public System.String Diagram { get; set;}
		/// <summary>Gets or sets the IllustrationId field. </summary>
		public System.Int32 IllustrationId { get; set;}
		/// <summary>Gets or sets the ModifiedDate field. </summary>
		public System.DateTime ModifiedDate { get; set;}
		/// <summary>Represents the navigator which is mapped onto the association 'ProductModelIllustration.Illustration - Illustration.ProductModelIllustrations (m:1)'</summary>
		public virtual List<ProductModelIllustration> ProductModelIllustrations { get; set;}
	}
}