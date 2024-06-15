import * as signalR from "@microsoft/signalr";

export const createSignalRConnection = (
  session: string | null,
): signalR.HubConnection => {
  const connection = new signalR.HubConnectionBuilder()
    .withUrl(`${process.env.NEXT_PUBLIC_BASE_URL}notifications`, {
      accessTokenFactory: () => session ?? "",
    })
    .withAutomaticReconnect()
    .build();

  connection
    .start()
    .then(() => console.log("SignalR Connected"))
    .catch((err) => console.log("SignalR Connection Error:", err));

  return connection;
};
