### Curso:

Analise meu schema do projeto

### Schema do Projeto:

model Course{
  id             Int         @id @default(autoincrement())
  name           String      
  description    String
  urlImg         String      @map("url_img") @default("")
  workload       Float?
  ranking        Int        @default(0)
  fieldOfStudy   String     @map("Field_of_study")
   
  categories     Category[]  @relation("category_course")
  companyId       Int        @map("company_id")
  company       Company    @relation(fields: [companyId], references: [id])

  ownerId       Int?       @map("owner_id")
  owner         User?      @relation("course_owner", fields: [ownerId], references: [id])

  
  @@map("courses")



  createdAt      DateTime    @default(now())
  updatedAt      DateTime    @updatedAt

}



