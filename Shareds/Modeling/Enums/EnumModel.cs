using System;
using System.Collections.Generic;
using System.Linq;

namespace Shareds.Modeling.Enums
{
    /// <summary>
    /// An abstract class to represent a set of GUIDs as an enum.
    /// </summary>
    /// <typeparam name="T">An enum to use for the type to expose.</typeparam>
    public abstract class EnumModel<T>
    {
        /// <summary>
        /// Fill up the list which matches GUIDs to the desired enum types.
        /// </summary>
        /// <param name="types"></param>
        protected abstract void FillTypes(Dictionary<Guid, T> types);

        /// <summary>
        /// List matching GUIDs with the enum types.
        /// </summary>
        private static Dictionary<Guid, T> types;

        /// <summary>
        /// The internal GUID.
        /// </summary>
        public Guid Guid { get; private set; }
        /// <summary>
        /// Gets the code name of the enum.
        /// </summary>
        public int Code { get; private set; }
        /// <summary>
        /// Gets the name of the enum.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The enum type for this GUID.
        /// </summary>
        public T Type { get; private set; }
        /// <summary>
        /// Specifies whether the GUID is known as a specific type or not.
        /// </summary>
        public bool IsKnownType { get; private set; }

        /// <summary>
        /// Create a new GUID enum wrapping the given GUID.
        /// </summary>
        /// <param name="guid"></param>
        protected EnumModel(Guid guid)
        {
            Guid = guid;

            if (IsGuidKnownType(guid))
            {
                IsKnownType = true;
                Type = GetType(guid);
            }
            else
            {
                IsKnownType = false;
            }
        }
        /// <summary>
        /// Create a new GUID enum for the specified type.
        /// </summary>
        /// <param name="type"></param>
        protected EnumModel(T type)
        {
            Type = type;
            Guid = GetGuid(type);
        }


        /// <summary>
        /// Call to make sure the list of types is initialized.
        /// </summary>
        private void InitializeTypes()
        {
            if (types != null)
                return;

            types = new Dictionary<Guid, T>();
            FillTypes(types);
        }
        /// <summary>
        /// Return the guid for a given type.
        /// </summary>
        /// <param name="type">The Type to get the Guid for.</param>
        /// <returns>The Guid for the given type.</returns>
        /// <exception cref="InvalidCastException">
        ///     Thrown when no Guid exists for the given type.
        /// </exception>
        private Guid GetGuid(T type)
        {
            InitializeTypes();

            try
            {
                return types.Keys.First(guid => types[guid].Equals(type));
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidCastException("No Guid exists for the given type.");
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public bool IsGuidKnownType(Guid guid)
        {
            InitializeTypes();

            return types.ContainsKey(guid);
        }
        /// <summary>
        /// Returns the type for a given GUID.
        /// </summary>
        /// <param name="type">The GUID to get the type for.</param>
        /// <returns>The type for the given GUID.</returns>
        public T GetType(Guid type)
        {
            InitializeTypes();

            if (!types.ContainsKey(type))
            {
                throw new ArgumentException("No type is defined for the given GUID.", "type");
            }
            return types[type];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is EnumModel<T>))
                return false;


            EnumModel<T> guidObj = obj as EnumModel<T>;

            return Guid.Equals(guidObj.Guid);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return Guid.GetHashCode();
            }
        }
    }
}
