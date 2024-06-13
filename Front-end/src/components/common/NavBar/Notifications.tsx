import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import { MdNotifications } from "react-icons/md";
import { Button } from "@/components/ui/button";
import { useEffect, useState } from "react";
import { useAppDispatch, useAppSelector } from "@/store/hooks";
import { Badge } from "@/components/ui/badge";
import NotificationHub from "@/utils/signalrConfig";
import NotificationList from "./NotificationList";

type Props = {};

const Notifications = (props: Props) => {
  const session = useAppSelector((state) => state.auth.session);
  const dispatch = useAppDispatch();
  const unReadCount = useAppSelector(
    (state) => state.notifications.unReadCount,
  );
  const [notificationHub, setNotificationHub] = useState<NotificationHub>();

  useEffect(() => {
    if (session != null)
      setNotificationHub(new NotificationHub(session!, dispatch));

    return () => {
      notificationHub?.stop();
    };
  }, [session]);

  return (
    <DropdownMenu>
      <DropdownMenuTrigger asChild>
        <Button variant="ghost" className="rounded-full relative">
          {unReadCount > 0 && (
            <Badge
              className="absolute inline-flex items-center justify-center w-6 h-6 text-xs font-bold border-2 border-green rounded-full -top-1 -end-1 dark:border-gray-900 bg-primary  text-white"
              variant="outline"
            >
              {unReadCount}{" "}
            </Badge>
          )}
          <MdNotifications size={30} />
        </Button>
      </DropdownMenuTrigger>
      <DropdownMenuContent>
        <DropdownMenuLabel>Notifications</DropdownMenuLabel>
        <DropdownMenuSeparator />
        <NotificationList notificationHub={notificationHub} />
      </DropdownMenuContent>
    </DropdownMenu>
  );
};

export default Notifications;
