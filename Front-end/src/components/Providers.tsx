'use client'

import { ReactNode } from "react"
import { Provider } from 'react-redux'
import { store } from '../store'
import Layout from "./common/Layout"
import { ThemeProvider } from "./theme-provider"

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
        <Layout>
          {children}
        </Layout>
      </ThemeProvider>
    </Provider>
  )
}

export default Providers