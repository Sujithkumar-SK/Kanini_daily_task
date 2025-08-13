public interface IProject
{
  Task<IEnumerable<Project>> GetAllProject();
  Task<Project> GetProjectById(int id);
  Task<Project> AddProject(Project project);
  Task<Project> UpdateProject(int id, Project project);
  Task<bool> DeleteProject(int id);
}