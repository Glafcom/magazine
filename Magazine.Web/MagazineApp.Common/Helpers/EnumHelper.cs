using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.Common.Helpers {
    public class EnumHelper {
        public static IDictionary<int, string> GetEnumDictionary<TEnum>()
            where TEnum : struct {
            if (!typeof(TEnum).IsEnum)
                return null;

            var dictionary = Enum.GetValues(typeof(TEnum))
               .Cast<TEnum>()
               .ToDictionary(e => Convert.ToInt32(e), e => e.ToString());
            return dictionary;
        }

        public static List<int> GetIntArray(int? min = 0, int? max = 100) {
            var list = new List<int>();

            if (max < min)
                return list;

            for (int i = (int)min; i <= max; i++) {
                list.Add(i);
            }

            return list;
        }
    }
}
