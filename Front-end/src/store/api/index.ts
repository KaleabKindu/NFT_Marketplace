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
  CategoryCount,
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
  tagTypes: [
    "NFTs",
    "Owned",
    "Created",
    "Collections",
    "Users",
    "Followings",
    "Followers",
    "Bids",
    "Provenances",
  ],
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
      providesTags: (results, meta, args) => [{ id: args, type: "NFTs" }],
    }),
    toggleNFTlike: builder.mutation<void, string>({
      query: (id) => ({
        url: `assets/toggle-like/${id}`,
        method: "PUT",
      }),
      invalidatesTags: (results, meta, args) => [{ id: args, type: "NFTs" }],
    }),
    cancelAssetSale: builder.mutation<void, string>({
      query: (id) => ({
        url: `assets/sale/${id}`,
        method: "PUT",
      }),
      invalidatesTags: (results, meta, args) => [{ id: args, type: "NFTs" }],
    }),
    getAssets: builder.query<IAssetsPage, IFilter>({
      query: (params) => {
        const filter = queryString.stringify(params, {
          skipNull: true,
          skipEmptyString: true,
        });
        return `assets/all?${filter.toString()}`;
      },
      providesTags: ["NFTs"],
    }),
    getOwnedAssets: builder.query<IAssetsPage, string>({
      query: (address) => `assets/owned?address=${address}`,
      providesTags: (results, meta, args) => [{ id: args, type: "Owned" }],
    }),
    getCreatedAssets: builder.query<IAssetsPage, string>({
      query: (address) => `assets/created?address=${address}`,
      providesTags: (results, meta, args) => [{ id: args, type: "Created" }],
    }),
    getUserDetails: builder.query<User, string>({
      query: (address) => `auth/user/detail?address=${address}`,
      transformResponse(baseQueryReturnValue: any, meta, arg) {
        return baseQueryReturnValue.value;
      },
      providesTags: (results, meta, args) => [{ id: args, type: "Users" }],
    }),
    getProvenance: builder.query<
      IProvenancePage,
      { id: number; pageNumber: number; pageSize: number }
    >({
      query: ({ id, pageNumber, pageSize }) =>
        `provenance/${id}?pageNumber=${pageNumber}&pageSize=${pageSize}`,
      providesTags: (results, meta, args) => [
        { id: args.id, type: "Provenances" },
      ],
    }),
    getBids: builder.query<
      IBidPage,
      { id: number; pageNumber: number; pageSize: number }
    >({
      query: ({ id, pageNumber, pageSize }) =>
        `bids?tokenId=${id}&pageNumber=${pageNumber}&pageSize=${pageSize}`,
      providesTags: (results, meta, args) => [
        { id: args.id, type: "Provenances" },
      ],
    }),
    getCollections: builder.query<ICollectionsPage, IFilter>({
      query: (params) => {
        const filter = queryString.stringify(params, {
          skipNull: true,
          skipEmptyString: true,
        });
        return `collections?${filter.toString()}`;
      },
      providesTags: ["Collections"],
    }),
    getCollectionDetails: builder.query<ICollection, string>({
      query: (id) => `collections/${id}`,
      transformResponse(baseQueryReturnValue: any, meta, arg) {
        return baseQueryReturnValue.value;
      },
    }),
    createCollection: builder.mutation<
      ICollection,
      { name: string; description: string; avatar: string }
    >({
      query: (payload) => ({
        url: `collections`,
        method: "POST",
        body: payload,
      }),
      invalidatesTags: ["Collections"],
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
        `auth/users/network/${address}?type=${type}&pageNumber=${pageNumber}&pageSize=${pageSize}`,
      providesTags: (results, meta, args) => [
        {
          id: args.address,
          type: args.type === "followings" ? "Followings" : "Followers",
        },
      ],
    }),
    followUser: builder.mutation<void, { follower: string; followee: string }>({
      query: (args) => ({
        url: `auth/users/network/${args.followee}`,
        method: "POST",
      }),
      invalidatesTags: (results, meta, args) => [
        { id: args.follower, type: "Followings" },
        { id: args.followee, type: "Followers" },
      ],
    }),
    unFollowUser: builder.mutation<
      void,
      { unfollower: string; unfollowee: string }
    >({
      query: (args) => ({
        url: `auth/users/network/${args.unfollowee}`,
        method: "DELETE",
      }),
      invalidatesTags: (results, meta, args) => [
        { id: args.unfollower, type: "Followings" },
        { id: args.unfollowee, type: "Followers" },
      ],
    }),
    getTopCreators: builder.query<IUsersPage, { page: number; size: number }>({
      query: ({ page, size }) =>
        `users/top-creators?pageNumber=${page}&pageSize=${size}`,
      transformResponse: (baseQueryReturnValue: any) => {
        return baseQueryReturnValue.value;
      },
    }),
    getCategoryCount: builder.query<CategoryCount, void>({
      query: () => `assets/categories-asset-count`,
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
        `collections/trending?pageNumber=${page}&pageSize=${size}`,
      transformResponse: (baseQueryReturnValue: any) => {
        return baseQueryReturnValue.value;
      },
    }),
    editProfile: builder.mutation<string, User>({
      query: (payload) => ({
        url: `userprofile`,
        method: "PUT",
        body: {
          userName: payload.userName,
          bio: payload.bio,
          avatar: payload.avatar,
          twitter: payload.twitter,
          youtube: payload.youtube,
          telegram: payload.telegram,
          facebook: payload.facebook,
        },
      }),
      transformResponse(baseQueryReturnValue: any, meta, arg) {
        return baseQueryReturnValue.value.nonce;
      },
      invalidatesTags: (result, meta, args) => [
        { id: args.address, type: "Users" },
      ],
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
  useEditProfileMutation,
  useCreateCollectionMutation,
  useFollowUserMutation,
  useUnFollowUserMutation,
  useToggleNFTlikeMutation,
  useGetCreatedAssetsQuery,
  useGetOwnedAssetsQuery,
  useCancelAssetSaleMutation,
} = webApi;
