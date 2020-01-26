﻿namespace WeasylFs.Endpoints

open WeasylFs

module MessageSummary =
    type Response = {
        comments: int
        journals: int
        notifications: int
        submissions: int
        unread_notes: int
    }

    let AsyncExecute credentials =
        "api/messages/summary"
        |> WeasylUtil.CreateRequest credentials
        |> WeasylUtil.AsyncReadJson<Response>

    let ExecuteAsync credentials =
        AsyncExecute credentials
        |> Async.StartAsTask