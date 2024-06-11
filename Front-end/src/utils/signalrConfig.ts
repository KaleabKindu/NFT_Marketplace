import { addNotification, addNotifications, notificationRead, removeNotification, setUnReadcount } from '@/store/slice/notification';
import * as signalR from '@microsoft/signalr';
import { Dispatch } from '@reduxjs/toolkit';
import { INotification } from '../types';


const createSignalRConnection = (session: string, dispatch: Dispatch): signalR.HubConnection => {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl(`${process.env.NEXT_PUBLIC_BASE_URL}notifications`, {
            accessTokenFactory: () => session
        })
        .withAutomaticReconnect()
        .build();

    connection.on('RecieveNotification', (notification: INotification) => {
        console.log('RecieveNotification:', notification);
        dispatch(addNotification(notification));
    });

    connection.on('LoadNotifications', (notifications) => {
        console.log("LoadNotifications", notifications)
        dispatch(addNotifications(notifications))
    })

    connection.on("UnReadNotificationCount", (count) => {
        console.log("UnReadNotificationCount", count)
        dispatch(setUnReadcount(count))
    })

    connection.start()
        .then(() => console.log('SignalR Connected'))
        .catch(err => console.log('SignalR Connection Error:', err));

    return connection;
};

class NotificationHub {
    connection: signalR.HubConnection;
    dispatch: Dispatch;

    constructor(session: string, dispatch: Dispatch) {
        this.connection = createSignalRConnection(session, dispatch);
        this.dispatch = dispatch;
    }

    markAsRead = (id: number) => {
        this.connection.invoke('MarkAsRead', id).then(() => {
            this.dispatch(notificationRead(id))
        }).catch(err => console.log('MarkAsRead Error:', err));
    }

    delete = (id: number) => {
        this.connection.invoke('DeleteNotification', id).then(() => {
            this.dispatch(removeNotification(id))
        }).catch(err => console.log('DeleteNotification Error:', err));
    }

    getToNotifications = (pageNumber: Number, pageSize: Number) => {
        this.connection.invoke('GetNotifications', pageNumber, pageSize).then(() => {
            console.log('GetNotifications')
        }).catch(err => console.log('GetNotifications Error:', err));
    }



    stop = () => {
        this.connection.stop().then(() => console.log("SignalR Disconnected!")).catch(err => console.log('SignalR Connection Error:', err));
    }
}

export default NotificationHub;

