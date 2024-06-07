import {
  Address,
  Credentials,
  IAssetsPage,
  IUsersPage,
  ICollectionsPage,
  IFilter,
  NFT,
  IBidPage,
  IProvenancePage,
  User,
  ICollection,
} from "@/types";
import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import type { RootState } from "..";
import queryString from "query-string";

export const webApi = createApi({
  reducerPath: "webApi",
  baseQuery: fetchBaseQuery({
    baseUrl: process.env.NEXT_PUBLIC_BASE_URL,
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
      query: (id) => `assets/${id}`,
      transformResponse(baseQueryReturnValue: any, meta, arg) {
        return baseQueryReturnValue.value;
      },
    }),
    getAssets: builder.query<IAssetsPage, IFilter>({
      query: (params) => {
        const filter = queryString.stringify(params, {
          skipNull: true,
          skipEmptyString: true,
        });
        return `assets/all?${filter.toString()}`;
      },
    }),
    getUserDetails: builder.query<User, string>({
      query: (address) => `auth/user/detail?address=${address}`,
      transformResponse(baseQueryReturnValue: any, meta, arg) {
        return baseQueryReturnValue.value;
      },
    }),
    getProvenance: builder.query<
      IProvenancePage,
      { id: string; pageNumber: number; pageSize: number }
    >({
      query: ({ id, pageNumber, pageSize }) =>
        `provenance/${id}?pageNumber=${pageNumber}&pageSize=${pageSize}`,
    }),
    getBids: builder.query<
      IBidPage,
      { id: string; pageNumber: number; pageSize: number }
    >({
      query: ({ id, pageNumber, pageSize }) =>
        `bids?tokenId=${id}&pageNumber=${pageNumber}&pageSize=${pageSize}`,
    }),
    getCollections: builder.query<ICollectionsPage, IFilter>({
      query: (params) => {
        const filter = queryString.stringify(params, {
          skipNull: true,
          skipEmptyString: true,
        });
        return `collections?${filter.toString()}`;
      },
    }),
    getCollectionDetails: builder.query<ICollection, string>({
      query: (id) => `collections/${id}`,
      transformResponse(baseQueryReturnValue: any, meta, arg) {
        return baseQueryReturnValue.value;
      },
    }),
    getUsers: builder.query<IUsersPage, IFilter>({
      query: (params) => {
        const filter = queryString.stringify(params, {
          skipNull: true,
          skipEmptyString: true,
        });
        return `auth/users?${filter.toString()}`;
      },
    }),
    getUserNetworks: builder.query<
      IUsersPage,
      { address: string; type: string; pageNumber: number; pageSize: number }
    >({
      query: ({ address, type, pageNumber, pageSize }) =>
        `users/network/${address}?type=${type}&pageNumber=${pageNumber}&pageSize=${pageSize}`,
    }),
    getTopCreators: builder.query<IUsersPage, { page: number; size: number }>({
      query: ({ page, size }) =>
        `users/top-creators?pageNumber=${page}&pageSize=${size}`,
      transformResponse: (baseQueryReturnValue: any) => {
        return baseQueryReturnValue.value;
      },
    }),
    getCategoryCount: builder.query<{ name: string; count: number }[], void>({
      query: () => `category`,
      transformResponse: (baseQueryReturnValue: any) => {
        return baseQueryReturnValue.value;
      },
    }),
    getTrendingAssets: builder.query<NFT[], { page: number; size: number }>({
      query: ({ page, size }) =>
        `assets/trending?pageNumber=${page}&pageSize=${size}`,
      transformResponse: (baseQueryReturnValue: any) => {
        return baseQueryReturnValue.value;
      },
    }),
    getTrendingCollections: builder.query<
      ICollection[],
      { page: number; size: number }
    >({
      query: ({ page, size }) =>
        `collection/trending?pageNumber=${page}&pageSize=${size}`,
      transformResponse: (baseQueryReturnValue: any) => {
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
  useGetAssetsQuery,
  useGetUserDetailsQuery,
  useGetProvenanceQuery,
  useGetBidsQuery,
  useGetCollectionsQuery,
  useGetCollectionDetailsQuery,
  useGetUsersQuery,
  useGetUserNetworksQuery,
  useGetTopCreatorsQuery,
  useGetCategoryCountQuery,
  useGetTrendingAssetsQuery,
  useGetTrendingCollectionsQuery,
} = webApi;
