import { INotification } from "@/types";
import { timeAgo } from "@/utils";
import NotificationHub from "@/utils/signalrConfig";
import { useEffect, useState } from "react";
import { useInView } from "react-intersection-observer";
import { MdClose } from "react-icons/md";
import { FaEthereum } from "react-icons/fa6";
import { useAppSelector } from "@/store/hooks";
import { DropdownMenuSeparator } from "@radix-ui/react-dropdown-menu";

type Props = {
  notificationHub: NotificationHub | undefined;
}

const NotificationList = ({ notificationHub }: Props) => {

  const notifications = useAppSelector((state) => state.notifications.notifications);
  const notificationscount = useAppSelector((state) => state.notifications.count);
  const [readNotifications, setReadNotifications] = useState<INotification[]>([]);
  const [unReadNotifications, setUnReadNotifications] = useState<INotification[]>([]);
  const [page, setPage] = useState(1);

  const limit = 10;

  useEffect(() => {
    setReadNotifications(notifications.filter((notification) => notification.isRead));
    setUnReadNotifications(notifications.filter((notification) => !notification.isRead));
  }, [notifications])


  const { ref, inView } = useInView({
    threshold: 1,
  });


  useEffect(() => {
    if (inView && notificationscount > notifications.length) {
      setPage(Math.ceil(notifications.length / limit) + 1)
      notificationHub?.getToNotifications(page, limit)
    }
  }, [inView])

  return (
    <div className="max-h-[500px] overflow-y-auto">
      {
        unReadNotifications.map((notification) => (
          <NotificationListItem key={notification.id} notification={notification} notificationHub={notificationHub} />
        ))
      }
      {readNotifications.length > 0 && unReadNotifications.length > 0 && <DropdownMenuSeparator />}
      {
        readNotifications.map((notification) => (
          <NotificationListItem key={notification.id} notification={notification} notificationHub={notificationHub} />
        ))
      }
      <div ref={ref} />
    </div>
  )
}

type ItemProps = {
  notification: INotification;
  notificationHub: NotificationHub | undefined;
}
const NotificationListItem = ({ notification, notificationHub }: ItemProps) => {

  const { ref, inView } = useInView({
    threshold: 1,
  });

  const handleDeleteNotification = () => {
    notificationHub?.delete(notification.id);
  }

  useEffect(() => {
    if (inView && !notification.isRead) {
      notificationHub?.markAsRead(notification.id);
    }
  }, [inView])


  return (
    <div ref={notification.isRead ? undefined : ref} key={notification.id} className={`flex first:rounded-t-2xl hover:bg-secondary ${!notification.isRead ? "font-extrabold" : ""}`}>
      <FaEthereum className="my-auto ml-2" size={30} />

      <div className="p-3">
        <div> {notification.title}</div>
        <div>{notification.content}</div>
        <div className="text-sm font-medium leading-none">
          {timeAgo(notification.date)}
        </div>
      </div>
      <MdClose className="my-auto ml-auto mr-2 hover:bg-accent cursor-pointer" size={25} onClick={handleDeleteNotification} />
    </div >
  )
}

export default NotificationList;