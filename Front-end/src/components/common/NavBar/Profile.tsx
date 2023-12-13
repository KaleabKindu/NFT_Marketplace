import {
    Menu,
    MenuHandler,
    Avatar,
    Button,
    MenuList,
    Typography,
    MenuItem,
  } from "@material-tailwind/react";
import Link from "next/link";
import { FaUserAlt } from "react-icons/fa";
import { TbNetworkOff } from "react-icons/tb";


type Props = {}

const Profile = (props: Props) => {
    
  return (
    <Menu placement="bottom-end">
        <MenuHandler>
            <Button variant="text" className="rounded-full p-1">
                <Avatar 
                src="https://docs.material-tailwind.com/img/face-2.jpg" 
                alt="avatar" />
            </Button>
        </MenuHandler>
        <MenuList className="dark:bg-dark-card dark:border-dark-border">
            <div
            className="flex gap-3 items-center justify-center p-4"
            >
              <Avatar 
                className='cursor-pointer' 
                src="https://docs.material-tailwind.com/img/face-2.jpg" 
                alt="avatar" />
                <div>
                    <Typography className="text-center dark:text-dark-text" variant="h4">
                        Tony Stark
                    </Typography>
                    <Typography className="text-center truncate max-w-[10rem]" variant="small">
                        X03849938292020338778343
                    </Typography>
                </div>
            </div>
            <MenuItem className="dark:hover:bg-dark-hover-bg dark:focus:bg-dark-hover-bg dark:text-dark-text">
                <Link href='' className="flex items-center gap-3">
                    <FaUserAlt size={20} />
                    <Typography>
                        Profile
                    </Typography>
                </Link>
            </MenuItem>
            <MenuItem className="dark:hover:bg-dark-hover-bg dark:focus:bg-dark-hover-bg dark:text-dark-text">
                <Link href='' className="flex items-center gap-3">
                    <TbNetworkOff size={20} />
                    <Typography>
                        Disconnect
                    </Typography>
                </Link>
            </MenuItem>

        </MenuList>
    </Menu>
  );
}

export default Profile