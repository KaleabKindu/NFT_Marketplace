import Container from "@/components/common/Container";

export default function Layout({ children }: { children: React.ReactNode }) {
  return <Container className="pt-10">{children}</Container>;
}
