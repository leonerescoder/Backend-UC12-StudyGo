### Categoria

Analise meu schema do projeto e o contexto.md

### Codigo: C#

Faça como o 'exemplo.cs' e implemente para o meu projeto

### Schema do prjeto

model Category {
  id                Int         @id @default(autoincrement())
  name              String      @unique
  description       String
  courses           Course[]    @relation("category_course")
  users             User[]      @relation("user_category") 
  
  @@map("categories")

  createdAt         DateTime    @default(now())
  updatedAt         DateTime    @updatedAt

  
}

