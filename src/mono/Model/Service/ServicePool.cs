
using System;

namespace monotooth.Service
{
	
	/// <summary>A simple collection that holds services.</summary>
	/// <remarks>The current implementation holds only linux specific services.</remarks>
	public class ServicePool : System.Collections.CollectionBase
	{
		/// <summary>The default constructor.</summary>
		public ServicePool()
		{
		}
		/// <summary>Add a service to this pool.</summary>
		/// <param name="ser">A service to be added to the pool. </param>
		public virtual void Add(monotooth.Service.Service ser)
		{
			this.List.Add(ser);
		}
		/// <summary>Removes a service from the pool. </summary>
		/// <param name="ser">A service to be removed from the pool. </param>
		public virtual void Remove(monotooth.Service.Service ser)
		{
			this.List.Remove(ser);			
		}		
		/// <summary>The index-operator for this pool</summary>
		/// <param name="index">The index.</param>
		/// <value>A service</value>
		public virtual monotooth.Service.Service this[int index]
		{
			get
			{
				return (monotooth.Service.Service)this.List[index];
			}
			set
			{
				this.List.Add(value);
			}
		}
	}
}
