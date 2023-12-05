import { configureStore } from '@reduxjs/toolkit'
import { webApi } from './api'
import { setupListeners } from '@reduxjs/toolkit/query'

export const store = configureStore({
  reducer: {
    [webApi.reducerPath]: webApi.reducer,
    },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(webApi.middleware),
})

setupListeners(store.dispatch)

// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<typeof store.getState>
// Inferred type: {posts: PostsState, comments: CommentsState, users: UsersState}
export type AppDispatch = typeof store.dispatch