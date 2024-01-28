import { Address, Credentials } from '@/types'
import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import type { RootState } from '..'

export const webApi = createApi({
  reducerPath: 'webApi',
  baseQuery: fetchBaseQuery({ 
    baseUrl: 'https://nft-68yj.onrender.com/nft-gebeya/api/v1/', 
    prepareHeaders:(headers, { getState }) => {
    const token = (getState() as RootState).auth.session as string
    if (token) {
      headers.set('authorization', `${token}`)
    }
    return headers
    }

}),
  endpoints: (builder) => ({ 
    getNounce: builder.mutation<string, Address>({
      query:(address) => ({
        url:`auth/users/create-fetch?PublicAddress=${address}`,
        method:'POST',
      }),
      transformResponse(baseQueryReturnValue:any, meta, arg) {
        return baseQueryReturnValue.value.nonce
      },
    }),
    authenticateSignature: builder.mutation<any, Credentials>({
      query:(credentials) => ({
        url:`auth/users/authenticate`,
        method:'POST',
        body:{ ...credentials }
      }),
      transformResponse(baseQueryReturnValue:any, meta, arg) {
        return baseQueryReturnValue.value.accessToken
      },
    })
  }),
})


export const { useGetNounceMutation, useAuthenticateSignatureMutation  } = webApi