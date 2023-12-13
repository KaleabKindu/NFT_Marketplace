import { Avatar } from "../Avatar";
import Link from "next/link";
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuItem,
    DropdownMenuLabel,
    DropdownMenuSeparator,
    DropdownMenuTrigger,
  } from "@/components/ui/dropdown-menu"
  
import { MdNotifications } from "react-icons/md";


type Props = {}

const Notifications = (props: Props) => {
  return (
    <DropdownMenu>
        <DropdownMenuTrigger className="rounded-full">
            <MdNotifications size={30}/>
        </DropdownMenuTrigger>
        <DropdownMenuContent>
            <DropdownMenuLabel>Notifications</DropdownMenuLabel>
            <DropdownMenuSeparator />
            <DropdownMenuItem>
                <div className="p-3">
                     <div> Measure action your user...</div>
                     <div className="text-sm font-medium leading-none">4 minutes ago</div>
                 </div>
            </DropdownMenuItem>
        </DropdownMenuContent>
    </DropdownMenu>
    // <Menu >
    //     <MenuHandler>
    //         <Button variant="text" className="rounded-full p-3 dark:hover:bg-dark-hover-bg">
    //             <Badge className='bg-primary'>
    //                  <MdNotifications className=' dark:text-dark-text' size={30}/>
    //             </Badge>
    //         </Button>
    //     </MenuHandler>
    //     <MenuList className="dark:bg-dark-card dark:border-dark-border">
    //             <div className="p-3">
    //                 <Typography className="dark:text-dark-text" variant="paragraph">
    //                     Measure action your user...
    //                 </Typography>
    //                 <Typography className="dark:text-dark-text" variant="small">
    //                     4 minutes ago
    //                 </Typography>
    //             </div>
    //     </MenuList>
    // </Menu>
  )
}

export default Notifications