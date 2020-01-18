namespace WeasylFs.Endpoints

open WeasylFs

module Whoami =
    type Response = {
        login: string
        userid: int
    }

    let AsyncExecute credentials =
        "/api/whoami"
        |> Util.CreateRequest credentials
        |> Util.AsyncReadJson<Response>

    let ExecuteAsync credentials =
        AsyncExecute credentials
        |> Async.StartAsTask