import { INotification } from "@/types";
import { timeAgo } from "@/utils";
import { useCallback, useContext, useEffect, useState } from "react";
import { useInView } from "react-intersection-observer";
import { MdClose } from "react-icons/md";
import { FaEthereum } from "react-icons/fa6";
import { useAppDispatch, useAppSelector } from "@/store/hooks";
import { DropdownMenuSeparator } from "@radix-ui/react-dropdown-menu";
import {
  notificationRead,
  removeNotification,
} from "@/store/slice/notification";
import { SocketContext } from "@/context/SocketContext";

type Props = {};

const NotificationList = ({}: Props) => {
  const notifications = useAppSelector(
    (state) => state.notifications.notifications,
  );
  const notificationscount = useAppSelector(
    (state) => state.notifications.count,
  );
  const [readNotifications, setReadNotifications] = useState<INotification[]>(
    [],
  );
  const [unReadNotifications, setUnReadNotifications] = useState<
    INotification[]
  >([]);
  const [page, setPage] = useState(1);
  const dispatch = useAppDispatch();
  const { socketConnection } = useContext(SocketContext);

  const markAsRead = useCallback(
    (id: number) => {
      socketConnection
        ?.invoke("MarkAsRead", id)
        .then(() => {
          dispatch(notificationRead(id));
        })
        .catch((err) => console.log("MarkAsRead Error:", err));
    },
    [socketConnection, dispatch],
  );

  const deleteNotifcation = useCallback(
    (id: number) => {
      socketConnection
        ?.invoke("DeleteNotification", id)
        .then(() => {
          dispatch(removeNotification(id));
        })
        .catch((err) => console.log("DeleteNotification Error:", err));
    },
    [socketConnection, dispatch],
  );

  const getNotifications = useCallback(
    (pageNumber: Number, pageSize: Number) => {
      socketConnection
        ?.invoke("GetNotifications", pageNumber, pageSize)
        .then(() => {
          console.log("GetNotifications");
        })
        .catch((err) => console.log("GetNotifications Error:", err));
    },
    [socketConnection, dispatch],
  );

  const limit = 10;

  useEffect(() => {
    setReadNotifications(
      notifications.filter((notification) => notification.isRead),
    );
    setUnReadNotifications(
      notifications.filter((notification) => !notification.isRead),
    );
  }, [notifications]);

  const { ref, inView } = useInView({
    threshold: 1,
  });

  useEffect(() => {
    if (inView && notificationscount > notifications.length) {
      setPage(Math.ceil(notifications.length / limit) + 1);
      getNotifications(page, limit);
    }
  }, [inView]);

  return (
    <div className="max-h-[500px] max-w-[600px] overflow-y-auto p-3">
      {unReadNotifications.map((notification) => (
        <NotificationListItem
          key={notification.id}
          notification={notification}
          markAsRead={markAsRead}
          deleteNotification={deleteNotifcation}
        />
      ))}
      {readNotifications.length > 0 && unReadNotifications.length > 0 && (
        <DropdownMenuSeparator />
      )}
      {readNotifications.map((notification) => (
        <NotificationListItem
          key={notification.id}
          notification={notification}
          markAsRead={markAsRead}
          deleteNotification={deleteNotifcation}
        />
      ))}
      {unReadNotifications.length === 0 && readNotifications.length === 0 && 
        <div className="text-center w-full py-10 font-semibold text-lg">No notifications</div>
      }
      <div ref={ref} />
    </div>
  );
};

type ItemProps = {
  notification: INotification;
  markAsRead: (id: number) => void;
  deleteNotification: (id: number) => void;
};

const NotificationListItem = ({
  notification,
  markAsRead,
  deleteNotification,
}: ItemProps) => {
  const { ref, inView } = useInView({
    threshold: 1,
  });

  const handleDeleteNotification = () => {
    deleteNotification(notification.id);
  };

  useEffect(() => {
    if (inView && !notification.isRead) {
      markAsRead(notification.id);
    }
  }, [inView]);

  return (
    <div
      ref={notification.isRead ? undefined : ref}
      key={notification.id}
      className={`flex gap-3 rounded-lg hover:bg-secondary ${!notification.isRead ? "font-extrabold" : ""}`}
    >
      <FaEthereum className="flex-initial my-auto ml-2" size={40} />

      <div className="flex-1 p-3">
        <div> {notification.title}</div>
        <div>{notification.content}</div>
        <div className="text-sm font-medium leading-none">
          {timeAgo(notification.date)}
        </div>
      </div>
      <MdClose
        className="flex-initial my-auto ml-auto mr-2 hover:bg-accent cursor-pointer"
        size={30}
        onClick={handleDeleteNotification}
      />
    </div>
  );
};

export default NotificationList;
