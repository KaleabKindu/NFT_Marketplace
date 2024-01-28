import { PayloadAction, createSlice } from "@reduxjs/toolkit";
import { PURGE } from "redux-persist";

interface authState {
    session:string | null
}
const initialState:authState = {
    session:null
}
export const authSlice = createSlice({
    name:'auth',
    initialState:initialState,
    reducers:{
        setSession(state, action:PayloadAction<string>){
            state.session = action.payload
        }
    },
    extraReducers: (builder) => {
        builder.addCase(PURGE, (state) => {
            return initialState
        });
    }
})

export const { setSession } = authSlice.actions