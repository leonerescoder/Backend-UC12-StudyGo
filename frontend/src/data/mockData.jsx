import React from 'react';

export const COURSES_DATA = [
  {
    id: 1,
    title: "Lógica de Programação",
    category: "Tecnologia da Informação",
    workload: "80 horas",
    badge: "🔥 Mais procurado",
    badgeType: "popular",
    school: "TechAcademy Pro",
    rating: 4.9,
    studentsCount: 1420,
    description: "Aprenda os fundamentos sólidos da lógica de programação estruturada, algoritmos, variáveis, estruturas condicionais e de repetição usando pseudocódigo e exemplos práticos.",
    modules: [
      "Introdução aos Algoritmos e Pensamento Computacional",
      "Variáveis, Tipos de Dados e Operadores",
      "Estruturas Condicionais (If/Else, Switch)",
      "Laços de Repetição (While, For)",
      "Vetores, Matrizes e Funções"
    ],
    renderBanner: () => (
      <svg viewBox="0 0 400 200" xmlns="http://www.w3.org/2000/svg" className="course-banner-svg">
        <defs>
          <linearGradient id="codeGrad" x1="0%" y1="0%" x2="100%" y2="100%">
            <stop offset="0%" stopColor="#0f172a" />
            <stop offset="100%" stopColor="#1e1b4b" />
          </linearGradient>
        </defs>
        <rect width="400" height="200" fill="url(#codeGrad)" />
        <circle cx="340" cy="40" r="80" fill="#00e5ff" opacity="0.08" filter="blur(30px)" />
        <rect x="40" y="30" width="320" height="145" rx="8" fill="#090d16" stroke="rgba(0,229,255,0.25)" strokeWidth="1.5" />
        <circle cx="60" cy="46" r="4" fill="#ef4444" />
        <circle cx="74" cy="46" r="4" fill="#f59e0b" />
        <circle cx="88" cy="46" r="4" fill="#10b981" />
        <text x="60" y="75" fill="#38bdf8" fontFamily="monospace" fontSize="12" fontWeight="600">&lt;code&gt;</text>
        <rect x="60" y="88" width="120" height="6" rx="3" fill="#00e5ff" opacity="0.8" />
        <rect x="60" y="104" width="180" height="6" rx="3" fill="#818cf8" opacity="0.7" />
        <rect x="80" y="120" width="140" height="6" rx="3" fill="#34d399" opacity="0.7" />
        <rect x="60" y="136" width="90" height="6" rx="3" fill="#f43f5e" opacity="0.8" />
        <text x="310" y="150" fill="#00e5ff" fontFamily="monospace" fontSize="28" fontWeight="bold" opacity="0.4">&lt;/&gt;</text>
      </svg>
    )
  },
  {
    id: 2,
    title: "Java do Zero ao Avançado",
    category: "Tecnologia da Informação",
    workload: "120 horas",
    badge: "⭐ Destaque",
    badgeType: "featured",
    school: "DevMaster Institute",
    rating: 4.8,
    studentsCount: 980,
    description: "Domine a linguagem Java moderna, Programação Orientada a Objetos (POO), Collections, Streams API, Spring Boot, JPA/Hibernate e boas práticas corporativas.",
    modules: [
      "Sintaxe Essencial e Tipos Primitivos",
      "Orientação a Objetos: Classes, Interfaces e Herança",
      "Coleções (List, Set, Map) e Generics",
      "Streams, Lambdas e Java Funcional",
      "Conexão com Banco de Dados e Spring Boot Básico"
    ],
    renderBanner: () => (
      <svg viewBox="0 0 400 200" xmlns="http://www.w3.org/2000/svg" className="course-banner-svg">
        <defs>
          <linearGradient id="javaGrad" x1="0%" y1="0%" x2="100%" y2="100%">
            <stop offset="0%" stopColor="#0f172a" />
            <stop offset="100%" stopColor="#2d1203" />
          </linearGradient>
        </defs>
        <rect width="400" height="200" fill="url(#javaGrad)" />
        <circle cx="200" cy="100" r="70" fill="#f97316" opacity="0.12" filter="blur(25px)" />
        <g transform="translate(160, 45)">
          <path d="M15,5 C10,18 25,25 20,38" stroke="#f97316" strokeWidth="3" fill="none" strokeLinecap="round" opacity="0.8" />
          <path d="M30,2 C25,16 40,24 35,38" stroke="#00e5ff" strokeWidth="3" fill="none" strokeLinecap="round" opacity="0.9" />
          <path d="M45,8 C42,20 54,26 50,38" stroke="#ef4444" strokeWidth="3" fill="none" strokeLinecap="round" opacity="0.7" />
          <path d="M10,45 L70,45 C70,75 58,95 40,95 C22,95 10,75 10,45 Z" fill="#1e293b" stroke="#f97316" strokeWidth="2" />
          <path d="M70,55 C82,55 82,75 70,75" stroke="#f97316" strokeWidth="3" fill="none" />
          <ellipse cx="40" cy="102" rx="35" ry="4" fill="#00e5ff" opacity="0.4" />
        </g>
        <text x="25" y="165" fill="#f97316" fontFamily="'Outfit', sans-serif" fontSize="14" fontWeight="700" letterSpacing="1">JAVA ENTERPRISE</text>
      </svg>
    )
  },
  {
    id: 3,
    title: "Desenvolvimento Web Completo",
    category: "Tecnologia da Informação",
    workload: "180 horas",
    badge: "🔥 Mais procurado",
    badgeType: "popular",
    school: "WebCraft Academy",
    rating: 5.0,
    studentsCount: 2310,
    description: "Construa sites e sistemas web interativos modernos e profissionais do início ao fim usando HTML5 Semântico, CSS3 Moderno, Flexbox/Grid e JavaScript ES6+.",
    modules: [
      "HTML5 Estruturado e Acessibilidade Web",
      "CSS3 Avançado, Flexbox, CSS Grid e Animações",
      "JavaScript Moderno (ES6+), DOM e Eventos",
      "Consumo de APIs REST com Fetch/Async-Await",
      "Projeto Prático Completo Responsivo"
    ],
    renderBanner: () => (
      <svg viewBox="0 0 400 200" xmlns="http://www.w3.org/2000/svg" className="course-banner-svg">
        <defs>
          <linearGradient id="webGrad" x1="0%" y1="0%" x2="100%" y2="100%">
            <stop offset="0%" stopColor="#090d16" />
            <stop offset="100%" stopColor="#06283d" />
          </linearGradient>
        </defs>
        <rect width="400" height="200" fill="url(#webGrad)" />
        <g transform="translate(60, 55)">
          <rect x="0" y="0" width="70" height="75" rx="10" fill="#e44d26" opacity="0.2" stroke="#e44d26" strokeWidth="1.5" />
          <text x="35" y="48" fill="#ff6b4a" fontFamily="'Outfit', sans-serif" fontSize="28" fontWeight="bold" textAnchor="middle">5</text>
          <text x="35" y="66" fill="#fca5a5" fontFamily="sans-serif" fontSize="9" textAnchor="middle">HTML5</text>
        </g>
        <g transform="translate(165, 55)">
          <rect x="0" y="0" width="70" height="75" rx="10" fill="#0284c7" opacity="0.2" stroke="#38bdf8" strokeWidth="1.5" />
          <text x="35" y="48" fill="#38bdf8" fontFamily="'Outfit', sans-serif" fontSize="28" fontWeight="bold" textAnchor="middle">3</text>
          <text x="35" y="66" fill="#bae6fd" fontFamily="sans-serif" fontSize="9" textAnchor="middle">CSS3</text>
        </g>
        <g transform="translate(270, 55)">
          <rect x="0" y="0" width="70" height="75" rx="10" fill="#ca8a04" opacity="0.2" stroke="#facc15" strokeWidth="1.5" />
          <text x="35" y="48" fill="#facc15" fontFamily="'Outfit', sans-serif" fontSize="24" fontWeight="bold" textAnchor="middle">JS</text>
          <text x="35" y="66" fill="#fef08a" fontFamily="sans-serif" fontSize="9" textAnchor="middle">ES6+</text>
        </g>
        <text x="200" y="165" fill="#38bdf8" fontFamily="sans-serif" fontSize="12" textAnchor="middle" fontWeight="600">FULLSTACK FRONTEND</text>
      </svg>
    )
  },
  {
    id: 4,
    title: "Computação em Nuvem",
    category: "Tecnologia da Informação",
    workload: "100 horas",
    badge: "🚀 Em Alta",
    badgeType: "featured",
    school: "CloudScale Institute",
    rating: 4.9,
    studentsCount: 1150,
    description: "Conceitos essenciais de arquitetura em nuvem, microsserviços, máquinas virtuais, contêineres Docker, Kubernetes e gerenciamento escalável de infraestrutura.",
    modules: [
      "Conceitos de Nuvem (IaaS, PaaS, SaaS)",
      "Redes Virtuais, Segurança e Permissões IAM",
      "Armazenamento de Objetos e Bancos Gerenciados",
      "Contêineres com Docker e Orquestração",
      "Alta Disponibilidade e Escalabilidade Automática"
    ],
    renderBanner: () => (
      <svg viewBox="0 0 400 200" xmlns="http://www.w3.org/2000/svg" className="course-banner-svg">
        <defs>
          <linearGradient id="cloudGrad" x1="0%" y1="0%" x2="100%" y2="100%">
            <stop offset="0%" stopColor="#0a0f1d" />
            <stop offset="100%" stopColor="#0f2b48" />
          </linearGradient>
        </defs>
        <rect width="400" height="200" fill="url(#cloudGrad)" />
        <circle cx="200" cy="85" r="50" fill="#00e5ff" opacity="0.1" filter="blur(20px)" />
        <g transform="translate(130, 45)">
          <path d="M35 60 A25 25 0 0 1 65 30 A35 35 0 0 1 125 40 A28 28 0 0 1 135 65 A25 25 0 0 1 125 90 L35 90 A25 25 0 0 1 35 60 Z" fill="#0e2238" stroke="#00e5ff" strokeWidth="2" />
          <rect x="45" y="105" width="55" height="12" rx="3" fill="#1e293b" stroke="#38bdf8" strokeWidth="1" />
          <circle cx="52" cy="111" r="2" fill="#10b981" />
          <line x1="72" y1="90" x2="72" y2="105" stroke="#00e5ff" strokeDasharray="2,2" strokeWidth="1.5" />
        </g>
        <text x="200" y="175" fill="#00e5ff" fontFamily="'Outfit', sans-serif" fontSize="13" textAnchor="middle" fontWeight="600">CLOUD ARCHITECTURE</text>
      </svg>
    )
  },
  {
    id: 5,
    title: "Banco de Dados SQL e NoSQL",
    category: "Tecnologia da Informação",
    workload: "60 horas",
    badge: "Novo",
    badgeType: "featured",
    school: "DataCore School",
    rating: 4.7,
    studentsCount: 650,
    description: "Aprenda a modelar, consultar e otimizar bancos relacionais como PostgreSQL/MySQL e não-relacionais como MongoDB e Redis.",
    modules: [
      "Modelagem de Dados e Normalização",
      "Comandos SQL (DDL, DML, Joins, Triggers)",
      "Indexação e Otimização de Performance",
      "Bancos Não Relacionais com MongoDB",
      "Modelos Chave-Valor com Redis"
    ],
    renderBanner: () => (
      <svg viewBox="0 0 400 200" xmlns="http://www.w3.org/2000/svg" className="course-banner-svg">
        <defs>
          <linearGradient id="dbGrad" x1="0%" y1="0%" x2="100%" y2="100%">
            <stop offset="0%" stopColor="#0d1117" />
            <stop offset="100%" stopColor="#161b22" />
          </linearGradient>
        </defs>
        <rect width="400" height="200" fill="url(#dbGrad)" />
        <g transform="translate(150, 45)">
          <ellipse cx="50" cy="20" rx="45" ry="14" fill="#1f293d" stroke="#38bdf8" strokeWidth="2" />
          <path d="M5,20 L5,50 C5,58 25,64 50,64 C75,64 95,58 95,50 L95,20" fill="none" stroke="#38bdf8" strokeWidth="2" />
          <path d="M5,50 L5,80 C5,88 25,94 50,94 C75,94 95,88 95,80 L95,50" fill="none" stroke="#00e5ff" strokeWidth="2" />
        </g>
        <text x="200" y="165" fill="#38bdf8" fontFamily="'Outfit', sans-serif" fontSize="13" textAnchor="middle" fontWeight="600">SQL & NoSQL DATABASES</text>
      </svg>
    )
  },
  {
    id: 6,
    title: "UI/UX Design e Prototipagem",
    category: "Design",
    workload: "75 horas",
    badge: "⭐ Destaque",
    badgeType: "featured",
    school: "DesignLab Brasil",
    rating: 4.9,
    studentsCount: 890,
    description: "Crie interfaces de usuário estonteantes e experiências memoráveis com Figma, design system, pesquisa com usuários e testes de usabilidade.",
    modules: [
      "Princípios de Design Visual e Hierarquia",
      "Figma do Básico a Componentes Avançados",
      "Criação de Design Systems Escaláveis",
      "Wireframes e Prototipação de Alta Fidelidade",
      "Testes de Usabilidade e Hand-off"
    ],
    renderBanner: () => (
      <svg viewBox="0 0 400 200" xmlns="http://www.w3.org/2000/svg" className="course-banner-svg">
        <defs>
          <linearGradient id="uxGrad" x1="0%" y1="0%" x2="100%" y2="100%">
            <stop offset="0%" stopColor="#140f2d" />
            <stop offset="100%" stopColor="#0a0e17" />
          </linearGradient>
        </defs>
        <rect width="400" height="200" fill="url(#uxGrad)" />
        <g transform="translate(140, 45)">
          <rect x="0" y="0" width="120" height="85" rx="8" fill="#1e1b4b" stroke="#a855f7" strokeWidth="2" />
          <circle cx="25" cy="30" r="12" fill="#ec4899" opacity="0.8" />
          <rect x="45" y="24" width="60" height="8" rx="4" fill="#c084fc" />
          <rect x="45" y="38" width="40" height="6" rx="3" fill="#818cf8" opacity="0.6" />
          <rect x="15" y="55" width="90" height="18" rx="5" fill="#00e5ff" opacity="0.3" />
        </g>
        <text x="200" y="165" fill="#c084fc" fontFamily="'Outfit', sans-serif" fontSize="13" textAnchor="middle" fontWeight="600">FIGMA & PRODUCT DESIGN</text>
      </svg>
    )
  }
];

export const SCHOOLS_DATA = [
  {
    rank: 1,
    name: "TechAcademy Pro",
    coursesCount: 42,
    rating: 4.9,
    reviewsCount: 3840,
    area: "Tecnologia e Engenharia"
  },
  {
    rank: 2,
    name: "DevMaster Institute",
    coursesCount: 35,
    rating: 4.85,
    reviewsCount: 2910,
    area: "Desenvolvimento de Software"
  },
  {
    rank: 3,
    name: "WebCraft Academy",
    coursesCount: 28,
    rating: 4.82,
    reviewsCount: 2430,
    area: "Web & Mobile"
  },
  {
    rank: 4,
    name: "CloudScale Institute",
    coursesCount: 24,
    rating: 4.79,
    reviewsCount: 1820,
    area: "DevOps & Cloud"
  },
  {
    rank: 5,
    name: "DesignLab Brasil",
    coursesCount: 19,
    rating: 4.75,
    reviewsCount: 1450,
    area: "Design & UX"
  }
];

export const CATEGORIES_DATA = [
  { id: "all", name: "Todos", icon: "🌐", count: "1000+ cursos" },
  { id: "ti", name: "Tecnologia da Informação", icon: "💻", count: "450 cursos" },
  { id: "design", name: "Design", icon: "🎨", count: "180 cursos" },
  { id: "gestao", name: "Gestão & Negócios", icon: "📊", count: "210 cursos" },
  { id: "marketing", name: "Marketing Digital", icon: "🚀", count: "140 cursos" },
  { id: "idiomas", name: "Idiomas", icon: "🗣️", count: "95 cursos" }
];
