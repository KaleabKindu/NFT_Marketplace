'use client'

import { ReactNode } from "react"
import { Provider } from 'react-redux'
import { store } from '../store'
import Layout from "./common/Layout"
import { ThemeProvider } from "./theme-provider"
import { Web3Modal } from "@/context/Web3Modal"
import ContractWrite from "@/context/ContractWrite"
type Props = {
    children:ReactNode
}

const Providers = ({ children }: Props) => {
  return (
    <Provider store={store}>
      <ThemeProvider
        attribute="class"
        defaultTheme="dark"
        enableSystem
        disableTransitionOnChange
      >
        <Web3Modal>
          <ContractWrite>
            <Layout>
              {children}
            </Layout>
          </ContractWrite>
        </Web3Modal>
      </ThemeProvider>
    </Provider>
  )
}

export default Providers