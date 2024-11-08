"use client";

import { ReactNode } from "react";
import { Provider } from "react-redux";
import { persistor, store } from "../store";
import Layout from "./common/Layout";
import { ThemeProvider } from "./theme-provider";
import { Web3Modal } from "@/context/Web3Modal";
import { PersistGate } from "redux-persist/integration/react";
import { SocketProvider } from "@/context/SocketContext";
type Props = {
  children: ReactNode;
};

const Providers = ({ children }: Props) => {
  return (
    <Provider store={store}>
      <PersistGate loading={null} persistor={persistor}>
        <ThemeProvider
          attribute="class"
          defaultTheme="dark"
          enableSystem
          disableTransitionOnChange
        >
          <Web3Modal>
            <SocketProvider>
              <Layout>{children}</Layout>
            </SocketProvider>
          </Web3Modal>
        </ThemeProvider>
      </PersistGate>
    </Provider>
  );
};

export default Providers;
