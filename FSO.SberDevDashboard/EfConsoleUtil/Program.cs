using FSO.SDD.DbModel;
using Microsoft.EntityFrameworkCore;
using System;

namespace EfConsoleUtil
{
    public class StoreContextUtil : DbContext
    {
        public DbSet<User> Users { get; set; }


        public DbSet<JiraEpic> JiraEpics { get; set; }
        public DbSet<JiraEpicTask> JiraEpicTasks { get; set; }

        public DbSet<JiraRelease> JiraReleases { get; set; }
        public DbSet<JiraReleaseHistory> JiraReleaseHistorys { get; set; }
        public DbSet<JiraReleaseState> JiraReleaseStates { get; set; }
        public DbSet<JiraReleaseTask> JiraReleaseTasks { get; set; }

        public DbSet<JiraSprint> JiraSprints { get; set; }
        public DbSet<JiraSprintHistory> JiraSprintHistorys { get; set; }
        public DbSet<JiraSprintSate> JiraSprintSatess { get; set; }
        public DbSet<JiraSprintTask> JiraSprintTasks { get; set; }

        public DbSet<JiraTask> JiraTasks { get; set; }
        public DbSet<JiraTaskHistory> JiraTaskHistorys { get; set; }
        public DbSet<JiraTaskState> JiraTaskStates { get; set; }

        public DbSet<SourceSystem> SourceSystems { get; set; }
        public DbSet<DataFact> DataFacts { get; set; }
        public DbSet<DataSource> DataSources { get; set; }


        public DbSet<GitBranch> GitBranches { get; set; }
        public DbSet<GitCommit> GitCommits { get; set; }
        public DbSet<GitCommitTask> GitCommitTasks { get; set; }

        public DbSet<GitPullRequest> GitPullRequests { get; set; }
        public DbSet<GitPullRequestApprover> GitPullRequestApprovers { get; set; }
        public DbSet<GitPullRequestState> GitPullRequestStates { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite(@"Data Source=FSO.SDD.DataBase.db;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<JiraEpic>().ToTable("JiraEpics");
            modelBuilder.Entity<JiraEpicTask>().ToTable("JiraEpicTasks");


            modelBuilder.Entity<JiraRelease>().ToTable("JiraReleases");
            modelBuilder.Entity<JiraRelease>().ToTable("JiraReleases");
            modelBuilder.Entity<JiraReleaseState>().ToTable("JiraReleaseStates");
            modelBuilder.Entity<JiraReleaseTask>().ToTable("JiraReleaseTasks");

            modelBuilder.Entity<JiraSprint>().ToTable("JiraSprints");
            modelBuilder.Entity<JiraSprintHistory>().ToTable("JiraSprintHistorys");
            modelBuilder.Entity<JiraSprintSate>().ToTable("JiraSprintSates");
            modelBuilder.Entity<JiraSprintTask>().ToTable("JiraSprintTasks");


            modelBuilder.Entity<JiraTask>().ToTable("JiraTasks");
            modelBuilder.Entity<JiraTaskHistory>().ToTable("JiraTaskHistorys");
            modelBuilder.Entity<JiraTaskState>().ToTable("JiraTaskStates");



            modelBuilder.Entity<SourceSystem>().ToTable("SourceSystems");
            modelBuilder.Entity<DataFact>().ToTable("DataFacts");
            modelBuilder.Entity<DataSource>().ToTable("DataSources");


            modelBuilder.Entity<GitBranch>().ToTable("GitBranches");
            modelBuilder.Entity<GitCommit>().ToTable("GitCommits");
            modelBuilder.Entity<GitCommitTask>().ToTable("GitCommitTasks");
            modelBuilder.Entity<GitPullRequest>().ToTable("GitPullRequests");
            modelBuilder.Entity<GitPullRequestApprover>().ToTable("GitPullRequestApprovers");
            modelBuilder.Entity<GitPullRequestState>().ToTable("GitPullRequestStates");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
