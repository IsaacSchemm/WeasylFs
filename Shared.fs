namespace WeasylFs

open System
open System.Net

module internal Shared =
    let weasyl_uri = new Uri("https://www.weasyl.com")

    let createRequest (credentials: IWeasylCredentials) (path: string) =
        let req = new Uri(weasyl_uri, path) |> WebRequest.CreateHttp
        req.Headers.Add("X-Weasyl-API-Key", credentials.ApiKey)
        req