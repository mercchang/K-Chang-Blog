//// -----------------------------------------------------------------------
//// <copyright file="App_Start.cs" company="Fluent.Infrastructure">
////     Copyright © Fluent.Infrastructure. All rights reserved.
//// </copyright>
////-----------------------------------------------------------------------
/// See more at: https://github.com/dn32/Fluent.Infrastructure/wiki
////-----------------------------------------------------------------------

using Fluent.Infrastructure.FluentTools;
using K_Chang_Blog.DataBase;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(K_Chang_Blog.App_Start), "PreStart")]

namespace K_Chang_Blog {
    public static class App_Start {
        public static void PreStart() {
            FluentStartup.Initialize(typeof(DbContextLocal));
        }
    }
}