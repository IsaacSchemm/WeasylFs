namespace WeasylFs.Endpoints

open WeasylFs
open System.Net

module UserView =
    let AsyncExecute credentials username =
        sprintf "api/users/%s/view" (WebUtility.UrlEncode username)
        |> WeasylUtil.CreateRequest credentials
        |> WeasylUtil.AsyncReadJson<User>

    let ExecuteAsync credentials username =
        AsyncExecute credentials username
        |> Async.StartAsTask