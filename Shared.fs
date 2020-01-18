namespace WeasylFs

open System
open System.Net

module internal Shared =
    let weasyl_uri = new Uri("https://www.weasyl.com")

    let createRequest (credentials: IWeasylCredentials) (path: string) =
        let req = new Uri(weasyl_uri, path) |> WebRequest.CreateHttp
        req.Headers.Add("X-Weasyl-API-Key", credentials.ApiKey)
        req

    let buildQueryString (parameters: (string * string) seq) =
        parameters
        |> Seq.map (fun (k, v) -> WebUtility.UrlEncode k, WebUtility.UrlEncode v)
        |> Seq.map (fun (k, v) -> sprintf "%s=%s" k v)
        |> String.concat "&"