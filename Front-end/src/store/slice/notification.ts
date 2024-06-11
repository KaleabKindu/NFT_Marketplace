import { INotification, INotificationPage } from "@/types";
import { PayloadAction, createSlice } from "@reduxjs/toolkit";
import { PURGE } from "redux-persist";


interface notificationState {
  notifications: INotification[];
  count: number;
  unReadCount: number;
}

const initialState: notificationState = {
  notifications: [],
  count: 0,
  unReadCount: 0,
}

export const notificationSlice = createSlice({
  name: 'notifications',
  initialState: initialState,
  reducers: {
    removeNotification(state, action: PayloadAction<number>) {
      if (state.notifications.find((notification) => notification.id === action.payload)?.isRead === false) {
        state.unReadCount -= 1;
      }
      state.notifications = state.notifications.filter((notification) => notification.id !== action.payload);
      state.count -= 1;
    },

    notificationRead(state, action: PayloadAction<number>) {
      state.notifications = state.notifications.map((notification) => {
        if (notification.id === action.payload) {
          notification.isRead = true;
        }
        return notification;
      });
      state.unReadCount -= 1;
    },

    setUnReadcount: (state, action: PayloadAction<number>) => {
      state.unReadCount = action.payload;
    },
    addNotification: (state, action: PayloadAction<INotification>) => {
      if (!state.notifications.find(x => x.id == action.payload.id)) {
        state.notifications = [action.payload, ...state.notifications];
        state.count += 1;
        state.unReadCount += 1;
      }
    },
    addNotifications: (state, action: PayloadAction<INotificationPage>) => {
      const { value, count, pageNumber, pageSize } = action.payload;
      state.count = count;
      if (state.notifications.length > 0 && pageNumber > 1 && (pageSize * pageNumber) > state.notifications.length) {
        state.notifications = [...state.notifications, ...value];
      }
      else {
        state.notifications = value;
      }
    },
  },
  extraReducers: (builder) => {
    builder.addCase(PURGE, (state) => {
      return initialState;
    });
  },
}
);

export const { addNotification, addNotifications, setUnReadcount, notificationRead, removeNotification } = notificationSlice.actions;

