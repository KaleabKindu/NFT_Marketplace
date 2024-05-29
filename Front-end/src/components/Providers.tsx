"use client";

import { ReactNode } from "react";
import { Provider } from "react-redux";
import { persistor, store } from "../store";
import Layout from "./common/Layout";
import { ThemeProvider } from "./theme-provider";
import { Web3Modal } from "@/context/Web3Modal";
import { PersistGate } from "redux-persist/integration/react";
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
            <Layout>{children}</Layout>
          </Web3Modal>
        </ThemeProvider>
      </PersistGate>
    </Provider>
  );
};

export default Providers;
