import {
    Menu,
    MenuHandler,
    MenuList,
    Badge,
    Typography,
    Button
  } from "@material-tailwind/react";
import { MdNotifications } from "react-icons/md";


type Props = {}

const Notifications = (props: Props) => {
  return (
    <Menu >
        <MenuHandler>
            <Button variant="text" className="rounded-full p-3 dark:hover:bg-dark-hover-bg">
                <Badge className='bg-primary'>
                     <MdNotifications className=' dark:text-dark-text' size={30}/>
                </Badge>
            </Button>
        </MenuHandler>
        <MenuList className="dark:bg-dark-card dark:border-dark-border">
                <div className="p-3">
                    <Typography className="dark:text-dark-text" variant="paragraph">
                        Measure action your user...
                    </Typography>
                    <Typography className="dark:text-dark-text" variant="small">
                        4 minutes ago
                    </Typography>
                </div>
        </MenuList>
    </Menu>
  )
}

export default Notifications