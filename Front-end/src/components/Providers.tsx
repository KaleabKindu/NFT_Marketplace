'use client'

import { ReactNode } from "react"
import { ThemeProvider } from './material-tailwind-components'
import { Provider } from 'react-redux'
import { store } from '../store'

type Props = {
    children:ReactNode
}

const Providers = ({ children }: Props) => {
  return (
    <Provider store={store}>
        <ThemeProvider>
            {children}
        </ThemeProvider>
    </Provider>
  )
}

export default Providers