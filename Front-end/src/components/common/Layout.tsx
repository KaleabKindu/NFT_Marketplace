import React, { ReactNode } from "react";
import NavBar from "./NavBar";
import Footer from "./Footer";

type Props = {
  children: ReactNode;
};

const Layout = ({ children }: Props) => {
  return (
    <>
      <NavBar />
      <div className="min-h-[90vh]">{children}</div>
      <Footer />
    </>
  );
};

export default Layout;
