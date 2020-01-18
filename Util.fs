﻿namespace WeasylFs

open System
open System.Net
open System.IO
open FSharp.Json

module Util =
    let DefaultWeasylUri = new Uri("https://www.weasyl.com")

    let CreateRequest (credentials: IWeasylCredentials) (path: string) =
        let req = new Uri(DefaultWeasylUri, path) |> WebRequest.CreateHttp
        req.Headers.Add("X-Weasyl-API-Key", credentials.ApiKey)
        req

    let AsyncReadJson<'a> (req: WebRequest) = async {
        let! resp = req.AsyncGetResponse()
        use sr = new StreamReader(resp.GetResponseStream())
        let! json = sr.ReadToEndAsync() |> Async.AwaitTask
        return Json.deserialize<'a> json
    }

    let BuildQueryString (parameters: (string * string) seq) =
        parameters
        |> Seq.map (fun (k, v) -> WebUtility.UrlEncode k, WebUtility.UrlEncode v)
        |> Seq.map (fun (k, v) -> sprintf "%s=%s" k v)
        |> String.concat "&"