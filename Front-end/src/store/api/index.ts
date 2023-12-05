import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'

export const webApi = createApi({
  reducerPath: 'webApi',
  baseQuery: fetchBaseQuery({ baseUrl: '/' }),
  endpoints: (builder) => ({ }),
})


export const {  } = webApi