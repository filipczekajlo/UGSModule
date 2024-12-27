using System;
using System.Collections.Generic;
using InventoryDTO.Weapons;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InventoryDTO
{
    public class ItemDataJsonConverter : JsonConverter
    {
            // here
        private static readonly Dictionary<string, Type> TypeMapping = new Dictionary<string, Type>
        {
            {StringConsts.BigBullet, typeof(BigBulletWeaponData)},
            {StringConsts.Cone, typeof(ConeWeaponData)},
            {StringConsts.Field, typeof(FieldWeaponData)},
            {StringConsts.Ground, typeof(GroundWeaponData)},
            {StringConsts.Heal, typeof(HealWeaponData)},
            {StringConsts.SmallBullet, typeof(SmallBulletWeaponData)},
            {StringConsts.Sprint, typeof(SprintWeaponData)},
            {StringConsts.Wall, typeof(WallWeaponData)},
            
            
            // {"ConeWeapon", typeof(ConeweaponData)},
            // {"DamageOverTimeWeapon", typeof(DamageOverTimeData)},
            // {"ThrowableWeapon", typeof(ThrowableWeaponData)},
            // {"GroundWeapon", typeof(GroundWeaponData)},
            // {"HealWeapon", typeof(HealWeaponData)},
            // {"HitScanWeapon", typeof(HitscanWeaponData)},
            // {"SprintWeapon", typeof(SprintWeaponData)}
        };

        public override bool CanConvert(Type objectType)
        {
            return typeof(ItemData).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var type = jsonObject["ItemType"]?.ToString();

            if (type != null && TypeMapping.TryGetValue(type, out var targetType))
            {
                var result = (ItemData)Activator.CreateInstance(targetType);
                serializer.Populate(jsonObject.CreateReader(), result);
                return result;
            }

            throw new JsonSerializationException($"Cannot deserialize! Type '{type}' is not supported");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            
            // Serialize the object as its specific type
            var jsonObject = JObject.FromObject(value, serializer);
            jsonObject.WriteTo(writer);
        }
            }
}