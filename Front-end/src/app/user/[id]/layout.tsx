import Container from "@/components/common/Container"
import Image from "next/image"


export default function Layout({
  children,
}: {
  children: React.ReactNode
}) {
  return (
    <div>
        <div className="relative w-full h-[30vh]">
            <Image className="object-cover" src='/collection/collection-background.jpeg' fill alt='background image'/>
        </div>
        <Container>
            {children}
        </Container>
    </div>
  )
}
