import {
    Drawer,
    Button,
    Typography,
    IconButton,
    Card,
    List,
    ListItem,
    ListItemPrefix,
    ListItemSuffix,
    Chip,
    Accordion,
    AccordionHeader,
    AccordionBody,

  } from "@material-tailwind/react";
  import { useEffect, useState } from 'react'
  import { RiMenu3Fill } from "react-icons/ri";
  import { FaUserAlt } from "react-icons/fa";
  import { MdNotifications } from "react-icons/md";
  import { TbNetworkOff } from "react-icons/tb";
  import { MdExplore } from "react-icons/md";
  import { IoMdClose } from "react-icons/io";
  import { FaChevronDown } from "react-icons/fa";
  import { MdDarkMode, MdLightMode } from "react-icons/md";

type Props = {
    darkMode:boolean,
    handleToggle:() => void
}

const SideBar = ({ darkMode, handleToggle }: Props) => {
    const [open, setOpen] = useState(false);
    const [index, setIndex] = useState(0);
    const openDrawer = () => setOpen(true);
    const closeDrawer = () => setOpen(false);
    const handleOpen = (index:number) => index === 0 ? setIndex(1):setIndex(0)

  return (
    <div className="lg:hidden">
          <IconButton variant="text" color="blue-gray" onClick={openDrawer}>
                <RiMenu3Fill size={30} />
          </IconButton>
      <Drawer open={open} onClose={closeDrawer} className="p-4 dark:bg-dark-card">
        <div className="h-[calc(100vh-2rem)] w-full max-w-[20rem]">
        <div className="flex justify-between mb-2 items-center">
            <Typography className=" dark:text-dark-text" variant="h5" color="blue-gray">
                NFT Marketplace
            </Typography>
            <IconButton className="dark:text-dark-text" variant="text" color="blue-gray" onClick={closeDrawer}>
                <IoMdClose size={30} />
            </IconButton>
        </div>
        <List className=" dark:text-dark-text">
            <ListItem className="dark:text-dark-text dark:hover:bg-dark-hover-bg">
            <ListItemPrefix>
                <MdExplore className="h-6 w-6" />
            </ListItemPrefix>
            Marketplace
            </ListItem>
            <Accordion
            className="dark:text-dark-text dark:hover:bg-dark-hover-bg" 
          open={index === 1}
          icon={
            
            <FaChevronDown
              strokeWidth={2.5}
              className={`mx-auto h-4 w-4 dark:text-dark-text transition-transform ${index === 1 ? "rotate-180" : ""}`}
            />
          }
        >
          <ListItem className="dark:text-dark-text dark:hover:bg-dark-hover-bg" selected={index === 1}>
            <AccordionHeader onClick={() => handleOpen(index)} className="border-b-0 py-0">
              <ListItemPrefix>
                <MdNotifications className="h-5 w-5 dark:text-dark-text" />
              </ListItemPrefix>
              <Typography color="blue-gray" className="mr-auto font-normal dark:text-dark-text">
                Notifications
              </Typography>
            </AccordionHeader>
          </ListItem>
          <AccordionBody>
            <List>
                <ListItem>
            <div className="">
                <Typography className="dark:text-dark-text" variant="paragraph">
                    Measure action your user...
                </Typography>
                <Typography className="dark:text-dark-text" variant="small">
                    4 minutes ago
                </Typography>
            </div>
                </ListItem>
            </List>
          </AccordionBody>
            </Accordion>
            <ListItem className="dark:text-dark-text dark:hover:bg-dark-hover-bg">
            <ListItemPrefix>
                <FaUserAlt className="h-6 w-6" />
            </ListItemPrefix>
            Profile
            </ListItem>
            <ListItem className="dark:text-dark-text dark:hover:bg-dark-hover-bg" onClick={handleToggle}>
            <ListItemPrefix>
                {darkMode ? <MdLightMode className='h-6 w-6'/>:  <MdDarkMode className='h-6 w-6'/>}
            </ListItemPrefix>
            {darkMode ? 'Light Mode':'Dark Mode'}
            </ListItem>
            <ListItem className="dark:text-dark-text dark:hover:bg-dark-hover-bg">
            <ListItemPrefix>
                <TbNetworkOff className="h-6 w-6" />
            </ListItemPrefix>
            Disconnect
            </ListItem>
        </List>
        </div>
      </Drawer>
    </div>
  )
}

export default SideBar