using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdminTerminal
{
    public static class Constant
    {
        public static string HelpString = String.Join(
        Environment.NewLine,
        "Usage: COMMAND RESOURCE [params..]",
        "",
        "Actions are based on single line console commands. COMMAND dictates the action to take and ",
        "the second word specifies from what RESOURCE the action is going to affect.",
        "",
        "COMMANDS:",
        "	> Add	[RESOURCE]",
        "	> Delete[RESOURCE]",
        "	> List 	[RESOURCE]",
        "	> Update[RESOURCE]",
        "",
        "RESOURCE:",
        "	> Articles 	[name] [price] [type] [ingredients]",
        "	> Employees	[name] [lastname] [email] [password]",
        "	> Ingredient[name] [price]",
        "	> Orders    [...]",
        "",
        "Examples:",
        "	> Add Article \"ExampleArticle\" \"99.0\" \"Food\" \"Cheese,Sausage\"",
        "	> Delete Employee 9",
        "	> List Ingredients",
        "",
        "Additonal flags",
        "	> --help|-h|--h			Displays this help menu",
        "	> --padding [int]        to be added",
        "",
        "Extras",
        "",
        "   > cls                   Clear out console");
    }
}
