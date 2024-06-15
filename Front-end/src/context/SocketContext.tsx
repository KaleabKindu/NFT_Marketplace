import { useAppSelector } from "@/store/hooks";
import { createSignalRConnection } from "@/utils/signalrConfig";
import { createContext, useState, ReactNode, useEffect, useRef } from "react";

interface SocketInitialState {
  socketConnection: signalR.HubConnection | null;
}

export const SocketContext = createContext<SocketInitialState>({
  socketConnection: null,
});

type Props = {
  children: ReactNode;
};

export const SocketProvider = ({ children }: Props) => {
  const session = useAppSelector((state) => state.auth.session);
  const [socketConnection, setSocketConnection] =
    useState<signalR.HubConnection | null>(null);
  const socketConnectionRef = useRef<signalR.HubConnection | null>(null);

  useEffect(() => {
    // Create a new SignalR connection with the current session
    const newSocketConnection = createSignalRConnection(session);
    setSocketConnection(newSocketConnection);
    socketConnectionRef.current = newSocketConnection;

    // Return a clean-up function that stops the SignalR connection
    return () => {
      // Check if the current socket connection exists and stop it
      socketConnectionRef.current
        ?.stop()
        .then(() => console.log("SignalR Disconnected!"))
        .catch((err) => console.log("SignalR Connection Error:", err));
    };
  }, [session]);

  return (
    <SocketContext.Provider value={{ socketConnection: socketConnection }}>
      {children}
    </SocketContext.Provider>
  );
};
