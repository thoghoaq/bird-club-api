namespace BirdClubAPI.Domain.Commons.Utils
{
    public class ExcludeNullPropertiesMapper
    {
        public static dynamic Map<T>(dynamic src, dynamic dest)
        {
            var requestProperties = typeof(T).GetProperties();
            foreach (var property in requestProperties)
            {
                var requestValue = property.GetValue(src);
                if (requestValue != null)
                {
                    var destProperty = dest.GetType().GetProperty(property.Name);
                    destProperty?.SetValue(dest, requestValue);
                }
            }
            return dest;
        }
    }
}
