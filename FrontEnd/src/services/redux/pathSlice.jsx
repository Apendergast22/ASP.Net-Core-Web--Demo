import { createSlice } from '@reduxjs/toolkit'

// Initial our user state.
const initialPathState = {
    current: "",
    previous: ""
}

export const pathSlice = createSlice({
    name: 'path',
    initialState: initialPathState, 
    reducers: {
        onNavigate: (state, param) => {
            const { payload } = param;
            state.previous = state.current;
            state.current = payload;
        }
    }
})

export const { onNavigate } = pathSlice.actions;
export default pathSlice.reducer
