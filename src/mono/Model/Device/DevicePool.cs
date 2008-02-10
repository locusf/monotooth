
using System;

namespace monotooth.Device
{
	
	/// <summary>A simple class to hold the devices in. </summary>
	public class DevicePool: System.Collections.CollectionBase
	{
		/// <summary>The default constructor.</summary>
		public DevicePool()
		{			
		}				
		/// <summary>Adds a device to this pool.</summary>
		/// <param name="dev">The device to add. </param>
		public virtual void Add(IRemoteDevice dev)
		{
			this.List.Add(dev);
		}
		/// <summary>Remove a device from the pool.</summary>
		/// <param name="dev">The device to remove from the pool.</param>
		public virtual void Remove(IRemoteDevice dev)
		{
			this.List.Remove(dev);			
		}		
		/// <summary>A special index operator. This is needed to make this collection
		/// <c>foreach</c> compatible. </summary>
		/// <param name="index">Index for device.</param>
		/// <value>A device.</value>
		public virtual IRemoteDevice this[int index]
		{
			get
			{
				return (IRemoteDevice)this.List[index];
			}
			set
			{
				this.List.Add(value);
			}
		}
	}
}
