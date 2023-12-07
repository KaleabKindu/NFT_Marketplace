'use client'

import { ReactNode } from "react"
import { ThemeProvider } from './material-tailwind-components'
import { Provider } from 'react-redux'
import { store } from '../store'
import Layout from "./common/Layout"

type Props = {
    children:ReactNode
}

const Providers = ({ children }: Props) => {
  return (
    <Provider store={store}>
        <ThemeProvider>
          <Layout>
            {children}
          </Layout>
        </ThemeProvider>
    </Provider>
  )
}

export default Providers