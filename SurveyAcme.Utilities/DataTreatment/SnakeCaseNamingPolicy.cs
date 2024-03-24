﻿using System.Text.Json;

namespace SurveyAcme.Utilities.DataTreatment
{
    public class SnakeCaseNamingPolicy : JsonNamingPolicy
    {
        public static SnakeCaseNamingPolicy Instance { get; } = new SnakeCaseNamingPolicy();

        public override string ConvertName(string name)
        {
            if (name.ToUpper().Equals("ID"))
                return "id";

            return name.ToSnakeCase();
        }
    }

    public static class StringUtils
    {
        public static string ToSnakeCase(this string str)
        {
            return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
        }
    }
}
