import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import { MdNotifications } from "react-icons/md";
import { Button } from "@/components/ui/button";
import { useAppSelector } from "@/store/hooks";
import { Badge } from "@/components/ui/badge";
import NotificationList from "./NotificationList";

type Props = {};

const Notifications = (props: Props) => {
  const unReadCount = useAppSelector(
    (state) => state.notifications.unReadCount,
  );

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
        <NotificationList />
      </DropdownMenuContent>
    </DropdownMenu>
  );
};

export default Notifications;
