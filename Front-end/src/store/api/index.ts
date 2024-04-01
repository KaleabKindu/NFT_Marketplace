import { Address, Credentials, NFT } from "@/types";
import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import type { RootState } from "..";

export const webApi = createApi({
  reducerPath: "webApi",
  baseQuery: fetchBaseQuery({
    baseUrl: "https://nft-68yj.onrender.com/nft-gebeya/api/v1/",
    prepareHeaders: (headers, { getState }) => {
      const token = (getState() as RootState).auth.session as string;
      if (token) {
        headers.set("authorization", `Bearer ${token}`);
      }
      return headers;
    },
  }),
  endpoints: (builder) => ({
    getNounce: builder.mutation<string, Address>({
      query: (address) => ({
        url: `auth/users/create-fetch?address=${address}`,
        method: "POST",
      }),
      transformResponse(baseQueryReturnValue: any, meta, arg) {
        return baseQueryReturnValue.value.nonce;
      },
    }),
    authenticateSignature: builder.mutation<any, Credentials>({
      query: (credentials) => ({
        url: `auth/users/authenticate`,
        method: "POST",
        body: { ...credentials },
      }),
      transformResponse(baseQueryReturnValue: any, meta, arg) {
        return baseQueryReturnValue.value.accessToken;
      },
    }),
    createNFT: builder.mutation<void, NFT>({
      query: (asset) => ({
        url: `assets/mint`,
        method: "POST",
        body: { ...asset },
      }),
      transformResponse(baseQueryReturnValue: any, meta, arg) {
        return baseQueryReturnValue.value;
      },
    }),
    getNFT: builder.query<NFT, string>({
      query: (id) => `/assets/${id}`,
      transformResponse(baseQueryReturnValue: any, meta, arg) {
        return baseQueryReturnValue.value;
      },
    }),
  }),
});

export const {
  useGetNounceMutation,
  useAuthenticateSignatureMutation,
  useCreateNFTMutation,
  useGetNFTQuery,
} = webApi;
