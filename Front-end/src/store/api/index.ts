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
  IUser,
} from "@/types";
import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import type { RootState } from "..";
import queryString from "query-string";

export const webApi = createApi({
  reducerPath: "webApi",
  baseQuery: fetchBaseQuery({
    baseUrl: "http://localhost/api/",
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
    getUserDetails: builder.query<void, string>({
      query: (address) => `users/${address}`,
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
        `bids/${id}?pageNumber=${pageNumber}&pageSize=${pageSize}`,
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
    getCollectionDetails: builder.query<void, string>({
      query: (id) => `collections/${id}`,
    }),
    getUsers: builder.query<IUsersPage, IFilter>({
      query: (params) => {
        const filter = queryString.stringify(params, {
          skipNull: true,
          skipEmptyString: true,
        });
        return `users?${filter.toString()}`;
      },
    }),
    getUserNetworks: builder.query<
      IUsersPage,
      { address: string; type: string; pageNumber: number; pageSize: number }
    >({
      query: ({ address, type }) => `users/network/${address}?type=${type}`,
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
    }),
    getTrendingAssets: builder.query<
      IAssetsPage,
      { page: number; size: number }
    >({
      query: ({ page, size }) =>
        `assets/trending?pageNumber=${page}&pageSize=${size}`,
      transformResponse: (baseQueryReturnValue: any) => {
        return baseQueryReturnValue.value;
      },
    }),
    getTrendingCollections: builder.query<
      ICollectionsPage,
      { page: number; size: number }
    >({
      query: ({ page, size }) =>
        `collections/trending?pageNumber=${page}&pageSize=${size}`,
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
