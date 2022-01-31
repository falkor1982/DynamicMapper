using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Globalization;
using System.Collections;

namespace DynamicMapper
{
    public static class Mapper
    {
        public enum MappingOptionFlags
        {
            MapCurrentEntityOnlyFlatTypes,
            MapCurrentEntityAndChildFlatTypes
        }

        #region Map List

        public static List<DtoType> MapListToDTO<DtoType, BusinessEntityType>(List<BusinessEntityType> inputBusinessEntityList,
            string cultureName) where DtoType : new()
        {
            List<DtoType> lista = new List<DtoType>();

            foreach (BusinessEntityType inputBusinessEntity in inputBusinessEntityList)
                lista.Add(MapToDTOInternal<DtoType, BusinessEntityType>(inputBusinessEntity, null, null, MappingOptionFlags.MapCurrentEntityOnlyFlatTypes, cultureName));
            return lista;
        }

        public static List<DtoType> MapListToDTO<DtoType, BusinessEntityType>(List<BusinessEntityType> inputBusinessEntityList,
            List<string> fillPropertyList, string cultureName) where DtoType : new()
        {
            List<DtoType> lista = new List<DtoType>();

            foreach (BusinessEntityType inputBusinessEntity in inputBusinessEntityList)
                lista.Add(MapToDTOInternal<DtoType, BusinessEntityType>(inputBusinessEntity, null, fillPropertyList, null, cultureName));
            return lista;
        }

        public static List<DtoType> MapListToDTO<DtoType, BusinessEntityType>(List<BusinessEntityType> inputBusinessEntityList,
            Dictionary<string, string> dtoTypePropertyToEntityPropertyMatching, List<string> fillPropertyList, string cultureName) where DtoType : new()
        {
            List<DtoType> lista = new List<DtoType>();

            foreach (BusinessEntityType inputBusinessEntity in inputBusinessEntityList)
                lista.Add(MapToDTOInternal<DtoType, BusinessEntityType>(inputBusinessEntity, dtoTypePropertyToEntityPropertyMatching, fillPropertyList, null, cultureName));
            return lista;
        }

        public static List<DtoType> MapListToDTO<DtoType, BusinessEntityType>(List<BusinessEntityType> inputBusinessEntityList,
            MappingOptionFlags mappingOptionFlag, string cultureName) where DtoType : new()
        {
            List<DtoType> lista = new List<DtoType>();

            foreach (BusinessEntityType inputBusinessEntity in inputBusinessEntityList)
                lista.Add(MapToDTOInternal<DtoType, BusinessEntityType>(inputBusinessEntity, null, null, mappingOptionFlag, cultureName));
            return lista;
        }

        public static List<DtoType> MapListToDTO<DtoType, BusinessEntityType>(List<BusinessEntityType> inputBusinessEntityList,
            Dictionary<string, string> dtoTypePropertyToEntityPropertyMatching, MappingOptionFlags mappingOptionFlag, string cultureName) where DtoType : new()
        {
            List<DtoType> lista = new List<DtoType>();

            foreach (BusinessEntityType inputBusinessEntity in inputBusinessEntityList)
                lista.Add(MapToDTOInternal<DtoType, BusinessEntityType>(inputBusinessEntity, dtoTypePropertyToEntityPropertyMatching, null, mappingOptionFlag, cultureName));
            return lista;
        }

        #endregion

        #region Map Object

        public static DtoType MapToDTO<DtoType, BusinessEntityType>(BusinessEntityType inputBusinessEntity,
            string cultureName) where DtoType : new()
        {
            return MapToDTOInternal<DtoType, BusinessEntityType>(inputBusinessEntity, null, null, MappingOptionFlags.MapCurrentEntityOnlyFlatTypes, cultureName);
        }

        public static DtoType MapToDTO<DtoType, BusinessEntityType>(BusinessEntityType inputBusinessEntity,
            Dictionary<string, string> dtoTypePropertyToEntityPropertyMatching, string cultureName, MappingOptionFlags mappingOptionFlag) where DtoType : new()
        {
            return MapToDTOInternal<DtoType, BusinessEntityType>(inputBusinessEntity, dtoTypePropertyToEntityPropertyMatching, null, mappingOptionFlag, cultureName);
        }

        public static DtoType MapToDTO<DtoType, BusinessEntityType>(BusinessEntityType inputBusinessEntity,
            Dictionary<string, string> dtoTypePropertyToEntityPropertyMatching, string cultureName, List<string> fillPropertyList) where DtoType : new()
        {
            return MapToDTOInternal<DtoType, BusinessEntityType>(inputBusinessEntity, dtoTypePropertyToEntityPropertyMatching, fillPropertyList, null, cultureName);
        }

        public static DtoType MapToDTO<DtoType, BusinessEntityType>(BusinessEntityType inputBusinessEntity,
            string cultureName, MappingOptionFlags mappingOptionFlag) where DtoType : new()
        {
            return MapToDTOInternal<DtoType, BusinessEntityType>(inputBusinessEntity, null, null, mappingOptionFlag, cultureName);
        }

        public static DtoType MapToDTO<DtoType, BusinessEntityType>(BusinessEntityType inputBusinessEntity,
            string cultureName, List<string> fillPropertyList) where DtoType : new()
        {
            return MapToDTOInternal<DtoType, BusinessEntityType>(inputBusinessEntity, null, fillPropertyList, null, cultureName);
        }

        #endregion

        private static DtoType[] MapArrayToDTOInternal<DtoType, BusinessEntityType>(List<BusinessEntityType> inputBusinessEntityList,
            Dictionary<string, string> dtoTypePropertyToEntityPropertyMatching, MappingOptionFlags mappingOptionFlag, List<string> fillPropertyList, string cultureName) where DtoType : new()
        {
            List<DtoType> listType = MapListToDTOInternal<DtoType, BusinessEntityType>(inputBusinessEntityList, dtoTypePropertyToEntityPropertyMatching, mappingOptionFlag, fillPropertyList, cultureName);
            return listType.ToArray();
        }

        private static List<DtoType> MapListToDTOInternal<DtoType, BusinessEntityType>(List<BusinessEntityType> inputBusinessEntityList,
            Dictionary<string, string> dtoTypePropertyToEntityPropertyMatching, MappingOptionFlags mappingOptionFlag, List<string> fillPropertyList, string cultureName) where DtoType : new()
        {
            if (inputBusinessEntityList == null)
                return null;

            List<DtoType> lista = new List<DtoType>();
            foreach (BusinessEntityType inputBusinessEntity in inputBusinessEntityList)
                lista.Add(MapToDTOInternal<DtoType, BusinessEntityType>(inputBusinessEntity, dtoTypePropertyToEntityPropertyMatching, fillPropertyList, mappingOptionFlag, cultureName));
            return lista;
        }

        /// <summary>
        /// Map the fields and children of an object, to its equivalent structure DTO
        /// </summary>
        /// <typeparam name="DtoType">DTO Type of which the method is to return a hydrated instance</typeparam>
        /// <typeparam name="BusinessEntityType">Type of which an instance is provided to map</typeparam>
        /// <param name="inputBusinessEntity">Instance of BusinessEntityType to be mapped</param>
        /// <param name="dtoTypePropertyToEntityPropertyMatching">Dictionary of equivalences between property names. Examples for KeyValuePairs are "Code","Code" and "User.Code","Usuario.Codigo". The key member is the </param>
        /// <param name="cultureName">Cultura con la que se convierten los valores a valor plano</param>
        /// <param name="fillPropertyList">Lista de objetos hijo que se deben mapear.</param>
        /// <param name="mappingOptionFlag">Opción estandard de mapeo para objetos hijo</param>
        /// <returns></returns>
        private static DtoType MapToDTOInternal<DtoType, BusinessEntityType>(BusinessEntityType inputBusinessEntity,
            Dictionary<string, string> dtoTypePropertyToEntityPropertyMatching, List<string> fillPropertyList, MappingOptionFlags? mappingOptionFlag, string cultureName) where DtoType : new()
        {
            if (inputBusinessEntity == null) return default(DtoType);

            Type realBusinessEntityType;
            DtoType outputDTOEntity;
            if (inputBusinessEntity.GetType() != typeof(BusinessEntityType)) //cuando la instancia de la entidad de negocio es una heredada de la base, busco que haya un dto con atributo específico que la representa
            {
                realBusinessEntityType = inputBusinessEntity.GetType();
                Type mirroredType = GetTypeWithAttributeValue<MirroredEntityAttribute>(Assembly.GetAssembly(typeof(DtoType)), attribute => attribute.BusinessEntityName, realBusinessEntityType, typeof(BusinessEntityType));
                outputDTOEntity = (DtoType)Activator.CreateInstance(mirroredType);
            }
            else if (typeof(DtoType).GetInterface("IGetEntity`1") != null) //Cuando en el dto implementa la interface IGetEntity del DtoMapper, instancio la clase que representa, y no la "base" de la propiedad
            {
                Type realType = typeof(DtoType).GetInterface("IGetEntity`1").GetGenericArguments()[0];
                outputDTOEntity = (DtoType)Activator.CreateInstance(realType);
                realBusinessEntityType = typeof(BusinessEntityType);
            }
            else
            {
                outputDTOEntity = new DtoType(); //instancio el dto que voy a devolver
                realBusinessEntityType = typeof(BusinessEntityType);

            }

            foreach (PropertyInfo dtoProperty in outputDTOEntity.GetType().GetProperties())
            {
                bool isCustomized;
                string businessEntityPropertyName = BusinessEntityMatchingProperty(dtoTypePropertyToEntityPropertyMatching, dtoProperty, out isCustomized);
                string invalidMappingMessageBase = "Invalid mapping: Property {0} was not found in type {1}";

                PropertyInfo propertyFromEntity = null;
                if (businessEntityPropertyName.Contains("."))
                {
                    if (!GetInnerMethod(businessEntityPropertyName, inputBusinessEntity, dtoTypePropertyToEntityPropertyMatching, fillPropertyList, mappingOptionFlag, cultureName, outputDTOEntity, dtoProperty, businessEntityPropertyName))
                        throw new Exception(string.Format(invalidMappingMessageBase, businessEntityPropertyName, outputDTOEntity.GetType().Name));
                }
                else
                {
                    propertyFromEntity = realBusinessEntityType.GetProperty(businessEntityPropertyName);
                    if (propertyFromEntity != null)
                    {
                        RedirectMapping<DtoType, BusinessEntityType>(inputBusinessEntity, dtoTypePropertyToEntityPropertyMatching, fillPropertyList, mappingOptionFlag, cultureName, outputDTOEntity, dtoProperty, businessEntityPropertyName, propertyFromEntity);
                    }
                    else
                    {
                        if (isCustomized)
                            throw new Exception(string.Format(invalidMappingMessageBase, businessEntityPropertyName, outputDTOEntity.GetType().Name));
                    }
                }
            }
            return outputDTOEntity;
        }

        public static Type GetTypeWithAttributeValue<TAttribute>(Assembly aAssembly, Func<TAttribute, object> pred, Type realBusinessEntityType, Type baseBusinessEntityType)
        {
            foreach (Type type in aAssembly.GetTypes())
            {
                foreach (TAttribute oTemp in type.GetCustomAttributes(typeof(TAttribute), true))
                {
                    if (Equals(pred(oTemp), realBusinessEntityType.Name))
                    {
                        return type;
                    }
                }
            }
            throw new Exception("Child classes must have MirroredEntity. None was found for type " + baseBusinessEntityType.Name);
        }

        private static void RedirectMapping<DtoType, BusinessEntityType>(BusinessEntityType inputBusinessEntity, Dictionary<string, string> dtoTypePropertyToEntityPropertyMatching, List<string> fillPropertyList, MappingOptionFlags? mappingOptionFlag, string cultureName, DtoType outputDTOEntity, PropertyInfo dtoProperty, string businessEntityPropertyName, PropertyInfo propertyFromEntity) where DtoType : new()
        {
            if (propertyFromEntity.PropertyType.Namespace == "System" || propertyFromEntity.PropertyType.IsEnum) // si la propiedad actual es un tipo básico (nulleable o no), lo asigno de la entidad, al dto
            {
                MapFlatValue<DtoType, BusinessEntityType>(inputBusinessEntity, cultureName, outputDTOEntity, dtoProperty, propertyFromEntity);
            }
            else
            {
                if (mappingOptionFlag.HasValue && mappingOptionFlag.Value == MappingOptionFlags.MapCurrentEntityAndChildFlatTypes ||
                           fillPropertyList != null && fillPropertyList.Contains(dtoProperty.Name)) //si no se da ninguna de estas dos condiciones, la propiedad hija no debe ser mapeada
                {
                    //al mapear recursivamente, a la lista de campos a llenar y al diccionario de macheos, le saco el prefijo de la entiddad propia
                    List<string> fillPropertyListChild = GetChildFillPropertyList(fillPropertyList, dtoProperty.Name);
                    Dictionary<string, string> childtoTypePropertyToEntityPropertyMatching =
                        GetChildToTypePropertyToEntityPropertyMatching(dtoTypePropertyToEntityPropertyMatching, businessEntityPropertyName, dtoProperty.Name);

                    if (propertyFromEntity.PropertyType.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICollection<>)))
                    {
                        Type argumentType;
                        if (propertyFromEntity.PropertyType.BaseType == typeof(Array))
                            argumentType = propertyFromEntity.PropertyType.GetElementType();
                        else
                            argumentType = propertyFromEntity.PropertyType.GetGenericArguments()[0]; //argumentType tiene el tipo de dato generico. Por ej en List<string>, queda string


                        if (argumentType.Namespace == "System" || argumentType.IsEnum) // si la lista es de tipos básicos, la paso como está al DTO
                        {
                            //Esto funciona con listas del mismo tipo. No funciona con convertir lista de enums a string, o lista de decimales a lista de strings. No lo arreglo porque es un caso muy inverosimil
                            var valueList = propertyFromEntity.GetValue(inputBusinessEntity, null);
                            if (dtoProperty.CanWrite)
                                dtoProperty.SetValue(outputDTOEntity, valueList, null);
                        }
                        else
                        {
                            MapCollection<DtoType, BusinessEntityType>(inputBusinessEntity, childtoTypePropertyToEntityPropertyMatching, fillPropertyListChild, mappingOptionFlag, cultureName, outputDTOEntity, dtoProperty, propertyFromEntity);
                        }
                    }
                    else
                    {
                        //Si la propiedad actual es un objeto de negocio, llamo recursivamente este metodo, mapeando esta propiedad hija. 
                        //Al ser este método genérico, el llamado recursivo lo debo hacer utilizando reflection
                        MapSingleBusinessChildObject<DtoType, BusinessEntityType>(inputBusinessEntity, childtoTypePropertyToEntityPropertyMatching, fillPropertyListChild, mappingOptionFlag, cultureName, outputDTOEntity, dtoProperty, propertyFromEntity);
                    }
                }
            }
        }

        private static bool GetInnerMethod<DtoType>(string pathName, object inputObject,
            Dictionary<string, string> dtoTypePropertyToEntityPropertyMatching, List<string> fillPropertyList, MappingOptionFlags? mappingOptionFlag, string cultureName, DtoType outputDTOEntity, PropertyInfo dtoProperty, string nombrePropiedadEntidadNegocio) where DtoType : new()
        {
            object currentObject = inputObject;
            string[] fieldNames = pathName.Split('.');
            int cantidad = fieldNames.Length;
            int actual = 0;
            string lastPropertyName = null;

            foreach (string fieldName in fieldNames)
            {
                actual++;
                Type curentRecordType = currentObject.GetType();
                PropertyInfo property = curentRecordType.GetProperty(fieldName);
                if (property != null)
                {
                    if (actual == cantidad)
                    {
                        Dictionary<string, string> dtoTypePropertyToEntityPropertyMatchingLocal = dtoTypePropertyToEntityPropertyMatching.Where(x => x.Value.StartsWith(lastPropertyName)).ToDictionary(p => p.Key, p => p.Value.Substring(lastPropertyName.Length + 1));
                        nombrePropiedadEntidadNegocio = nombrePropiedadEntidadNegocio.Substring(nombrePropiedadEntidadNegocio.IndexOf(".") + 1);
                        RedirectMapping<DtoType, object>(currentObject, dtoTypePropertyToEntityPropertyMatchingLocal, fillPropertyList, mappingOptionFlag, cultureName, outputDTOEntity, dtoProperty, nombrePropiedadEntidadNegocio, property);
                        return true;
                    }
                    currentObject = property.GetValue(currentObject, null);
                    lastPropertyName = fieldName;
                }
                else
                    return false;
            }
            return false;
        }

        private static void MapFlatValue<DtoType, BusinessEntityType>(BusinessEntityType inputBusinessEntity, string cultureName, DtoType outputDTOEntity, PropertyInfo dtoProperty, PropertyInfo propertyFromEntity) where DtoType : new()
        {
            if (dtoProperty.CanWrite)
            {
                if (dtoProperty.PropertyType == propertyFromEntity.PropertyType)
                    dtoProperty.SetValue(outputDTOEntity, propertyFromEntity.GetValue(inputBusinessEntity, null), null);
                else if ((dtoProperty.PropertyType == typeof(int) && propertyFromEntity.PropertyType.IsEnum) ||
                    (propertyFromEntity.PropertyType.IsGenericType &&
                    propertyFromEntity.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) &&
                    propertyFromEntity.PropertyType.GetGenericArguments()[0].IsEnum)) //cuando tengo un enum en la entidad de negocio, y un campo int en el dto, paso su valor numérico. Si ese enum es nulleable, chequeo el valor
                {
                    if (propertyFromEntity.GetValue(inputBusinessEntity, null) != null)
                    {
                        int enumIntValue = (int)propertyFromEntity.GetValue(inputBusinessEntity, null);
                        dtoProperty.SetValue(outputDTOEntity, Convert.ChangeType(enumIntValue, dtoProperty.PropertyType), null);
                    }
                }
                else
                {
                    string valueStr = ValueStr(propertyFromEntity, dtoProperty, inputBusinessEntity, cultureName);
                    bool isNullableType = (Nullable.GetUnderlyingType(dtoProperty.PropertyType) != null);//Si es una propiedad nulleable (int?, decimal?, bool?...), se asigna de forma diferente
                    if (isNullableType)
                    {
                        if (valueStr != null)
                        {
                            var nullType = Nullable.GetUnderlyingType(dtoProperty.PropertyType);
                            var notNullWithValue = Convert.ChangeType(valueStr, nullType);
                            dtoProperty.SetValue(outputDTOEntity, notNullWithValue, null);
                        }
                        else
                            dtoProperty.SetValue(outputDTOEntity, null, null);
                    }
                    else
                    {
                        dtoProperty.SetValue(outputDTOEntity, Convert.ChangeType(valueStr, dtoProperty.PropertyType), null);
                    }
                }
            }
        }

        private static void MapCollection<DtoType, BusinessEntityType>(BusinessEntityType inputBusinessEntity, Dictionary<string, string> dtoTypePropertyToEntityPropertyMatching, List<string> fillPropertyList, MappingOptionFlags? mappingOptionFlag, string cultureName, DtoType outputDTOEntity, PropertyInfo dtoProperty, PropertyInfo propertyFromEntity) where DtoType : new()
        {
            Type collectionTypeFather = propertyFromEntity.PropertyType;
            Type fatherType;
            if (collectionTypeFather.BaseType == typeof(Array))
                fatherType = collectionTypeFather.GetElementType();
            else
            {
                fatherType = collectionTypeFather.IsGenericType && !collectionTypeFather.IsGenericTypeDefinition
                    ? collectionTypeFather.GetGenericArguments()[0]
                    : Type.EmptyTypes[0];
            }
            Type collectionTypeChild = dtoProperty.PropertyType;
            string mappingCollectionMethod;
            Type dtoChildPropertyType;
            if (collectionTypeChild.BaseType == typeof(Array))
            {
                dtoChildPropertyType = collectionTypeChild.GetElementType();
                mappingCollectionMethod = "MapArrayToDTOInternal";
            }
            else
            {
                dtoChildPropertyType = collectionTypeChild.IsGenericType && !collectionTypeChild.IsGenericTypeDefinition
                   ? collectionTypeChild.GetGenericArguments()[0]
                   : Type.EmptyTypes[0];
                mappingCollectionMethod = "MapListToDTOInternal";
            }
            MethodInfo methodInfo = typeof(Mapper).GetMethod(mappingCollectionMethod, BindingFlags.NonPublic | BindingFlags.Static);
            Type[] typeParams = new Type[] { dtoChildPropertyType, fatherType };
            MethodInfo generic = methodInfo.MakeGenericMethod(typeParams);
            var entityChildObject = propertyFromEntity.GetValue(inputBusinessEntity, null);
            MappingOptionFlags? childMappingOptionFlag = ChildMappingOptionFlag(mappingOptionFlag);
            var returnValue = generic.Invoke(null, new object[] { entityChildObject, dtoTypePropertyToEntityPropertyMatching, childMappingOptionFlag, fillPropertyList, cultureName });
            if (dtoProperty.CanWrite)
                dtoProperty.SetValue(outputDTOEntity, returnValue, null);
        }

        private static void MapSingleBusinessChildObject<DtoType, BusinessEntityType>(BusinessEntityType inputBusinessEntity, Dictionary<string, string> dtoTypePropertyToEntityPropertyMatching, List<string> fillPropertyList, MappingOptionFlags? mappingOptionFlag, string cultureName, DtoType outputDTOEntity, PropertyInfo dtoProperty, PropertyInfo propertyFromEntity) where DtoType : new()
        {
            if (dtoProperty.CanWrite)
            {
                Type fatherType = propertyFromEntity.PropertyType;
                Type dtoChildPropertyType = dtoProperty.PropertyType;
                MethodInfo methodInfo = typeof(Mapper).GetMethod("MapToDTOInternal", BindingFlags.NonPublic | BindingFlags.Static);
                Type[] typeParams = new Type[] { dtoChildPropertyType, fatherType };
                MethodInfo generic = methodInfo.MakeGenericMethod(typeParams);
                var entityChildObject = propertyFromEntity.GetValue(inputBusinessEntity, null);
                MappingOptionFlags? childMappingOptionFlag = ChildMappingOptionFlag(mappingOptionFlag);
                var returnValue = generic.Invoke(null, new object[] { entityChildObject, dtoTypePropertyToEntityPropertyMatching, fillPropertyList, childMappingOptionFlag, cultureName });
                dtoProperty.SetValue(outputDTOEntity, returnValue, null);
            }
        }

        #region Needed stuff for recursive invocations

        private static MappingOptionFlags? ChildMappingOptionFlag(MappingOptionFlags? mappingOptionFlag)
        {
            MappingOptionFlags? childMappingOptionFlag = null;
            if (mappingOptionFlag.HasValue && mappingOptionFlag.Value == MappingOptionFlags.MapCurrentEntityAndChildFlatTypes)
                childMappingOptionFlag = MappingOptionFlags.MapCurrentEntityOnlyFlatTypes;
            return childMappingOptionFlag;
        }

        //devuelve para la propiedad que estoy revisando en el DTO, cómo se debe llamar la propiedad relacionada en la entidad de negocio
        private static string BusinessEntityMatchingProperty(Dictionary<string, string> dtoTypePropertyToEntityPropertyMatching, PropertyInfo dtoProperty, out bool isCustomized)
        {
            //isCustomized: devuelve si es un valor pedido en el diccionario. Si es asi, y despues no se encuentra, voy a tirar error de mapeo
            string propABuscar = string.Empty; //la propiedad a buscar en la entidad de negocio, es la misma que el dto si no esta en la lista a mapear
            if (dtoTypePropertyToEntityPropertyMatching != null && dtoTypePropertyToEntityPropertyMatching.ContainsKey(dtoProperty.Name))
            {
                isCustomized = true;
                propABuscar = dtoTypePropertyToEntityPropertyMatching[dtoProperty.Name];
            }
            else
            {
                isCustomized = false;
                propABuscar = dtoProperty.Name;
            }
            return propABuscar;
        }

        //voy a mapear recursivamente. Cuando la funcion llama a si misma, debe quitarle el prefijo al nombre de la propiedad sobre la que esta llamando.
        private static List<string> GetChildFillPropertyList(List<string> parentFillPropertyList, string dtoPropertyName)
        {
            if (parentFillPropertyList == null) return null;
            List<string> fillPropertyListChild = parentFillPropertyList.Where(item => item.StartsWith(dtoPropertyName + ".")).ToList();
            if (fillPropertyListChild.Count > 0)
                fillPropertyListChild = fillPropertyListChild.ConvertAll(d => d.Substring(dtoPropertyName.Length + 1));
            return fillPropertyListChild;
        }

        private static Dictionary<string, string> GetChildToTypePropertyToEntityPropertyMatching(Dictionary<string, string> parentdtoTypePropertyToEntityPropertyMatchin, string nombrePropiedadEntidadNegocio, string nombrePropiedadDto)
        {
            if (parentdtoTypePropertyToEntityPropertyMatchin == null) return null;
            Dictionary<string, string> childtoTypePropertyToEntityPropertyMatching = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> mapeoPropiedad in parentdtoTypePropertyToEntityPropertyMatchin)
            {
                if (mapeoPropiedad.Key.StartsWith(nombrePropiedadDto + "."))
                    childtoTypePropertyToEntityPropertyMatching.Add(mapeoPropiedad.Key.Substring(nombrePropiedadDto.Length + 1), mapeoPropiedad.Value.Substring(nombrePropiedadEntidadNegocio.Length + 1));
            }
            return childtoTypePropertyToEntityPropertyMatching;
        }

        #endregion

        //Devuelve el equivalente "plano" para cualquier tipo de dato basico de ,net (del namespace System)
        private static string ValueStr(PropertyInfo propertyFromEntity, PropertyInfo dtoProperty, object inputEntity, string cultureName)
        {
            if (propertyFromEntity.GetValue(inputEntity, null) == null) return null;
            CultureInfo culture = new CultureInfo(cultureName);
            string valueStr = null;
            if (propertyFromEntity.PropertyType == typeof(String))
                valueStr = (String)propertyFromEntity.GetValue(inputEntity, null);
            else if (propertyFromEntity.PropertyType == typeof(Guid))
                valueStr = (String)propertyFromEntity.GetValue(inputEntity, null).ToString();
            else if (propertyFromEntity.PropertyType == typeof(Int32) || propertyFromEntity.PropertyType == typeof(Int32?))
                valueStr = ((Int32)propertyFromEntity.GetValue(inputEntity, null)).ToString();
            else if (propertyFromEntity.PropertyType == typeof(Int16) || propertyFromEntity.PropertyType == typeof(Int16?))
                valueStr = ((Int16)propertyFromEntity.GetValue(inputEntity, null)).ToString();
            else if (propertyFromEntity.PropertyType == typeof(Decimal) || propertyFromEntity.PropertyType == typeof(Decimal?))
                valueStr = ((Decimal)propertyFromEntity.GetValue(inputEntity, null)).ToString("N2", culture);
            else if (propertyFromEntity.PropertyType == typeof(Boolean) || propertyFromEntity.PropertyType == typeof(Boolean?))
                valueStr = ((Boolean)propertyFromEntity.GetValue(inputEntity, null)).ToString();
            else if (propertyFromEntity.PropertyType.IsEnum)
                valueStr = ((Enum)propertyFromEntity.GetValue(inputEntity, null)).ToString();
            else if (propertyFromEntity.PropertyType == typeof(DateTime) || propertyFromEntity.PropertyType == typeof(DateTime?))
            {
                DateTime value = (DateTime)propertyFromEntity.GetValue(inputEntity, null);
                bool customizedDateTimeConversionAttribute = false;
                object[] attrs = dtoProperty.GetCustomAttributes(false);
                foreach (object attr in attrs)
                {
                    DateTimeConversionAttribute myAttr = attr as DateTimeConversionAttribute;
                    if (myAttr != null)
                    {
                        customizedDateTimeConversionAttribute = true;
                        if (myAttr.DateTimeConversionOption == DateTimeConversionOptionEnum.Iso8061FullDateTime)  // yyyy-MM-ddThh:mm:ss
                            valueStr = value.ToString("s", CultureInfo.InvariantCulture);
                        else if (myAttr.DateTimeConversionOption == DateTimeConversionOptionEnum.Iso8061OnlyDate) // yyyy-MM-dd
                            valueStr = value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        else //Format was assigned
                            valueStr = value.ToString(myAttr.Format, CultureInfo.InvariantCulture);
                        break;
                    }
                }
                if (!customizedDateTimeConversionAttribute) //Set default
                    valueStr = GetDefaultConvertedDateTime(value);
            }
            return valueStr;
        }

        /// <summary>
        /// Default conversion method for datetimes to string
        /// </summary>
        /// <param name="value">Datetime value</param>
        /// <returns></returns>
        private static string GetDefaultConvertedDateTime(DateTime value)
        {
            return value.ToString("s", System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}


