import Container from "@/components/common/Container"



export default function Layout({
  children,
}: {
  children: React.ReactNode
}) {
  return (
    <Container>
        {children}
    </Container>
  )
}
