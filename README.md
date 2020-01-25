WeasylFs
========

An F# / .NET wrapper library for the [Weasyl API](https://projects.weasyl.com/weasylapi/).

Each request is mapped to a module (static class) in the WeasylFs.Endpoints
namespace with `AsyncExecute` and `ExecuteAsync` functions (the former returns
an `Async<T>` for F# async workflows, and the latter returns a "hot"
`Task<T>`). Both functions take a credentials object as the first parameter:

    // C#
    var credentials = new WeasylFs.WeasylCredentials("api_key_goes_here");
    var user = await WeasylFs.Endpoints.UserView.ExecuteAsync(credentials, "Ikani");

    // F#
    let credentials = { api_key: "api_key_goes_here" }
    let! user = WeasylFs.Endpoints.UserView.AsyncExecute credentials "Ikani"

You can also implement `IWeasylCredentials` yourself if you already have an
object in your application that contains the Weasyl API key as one of its
properties. (Or, to access the Weasyl API without any credentials, use
`WeasylCredentials.None`).